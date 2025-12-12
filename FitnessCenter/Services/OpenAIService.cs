// FitnessCenter.Services/OpenAIService.cs
using FitnessCenter.Areas.User.Services;
using FitnessCenter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitnessCenter.Services
{
    public class OpenAIService : IAIService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _model;
        private readonly Random _rng = new();
        private readonly ILogger<OpenAIService> _logger;

        public OpenAIService(HttpClient http, IConfiguration config, ILogger<OpenAIService> logger)
        {
            _http = http;
            _logger = logger;
            _apiKey = config["OpenAI:ApiKey"] ?? "";
            _model = config["OpenAI:Model"] ?? "gpt-4o-mini";
            if (string.IsNullOrEmpty(_apiKey))
                throw new Exception("OpenAI:ApiKey is not configured.");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("FitnessCenter/1.0");
        }

        public async Task<string> GenerateRecommendationAsync(BodyProfile profile)
        {
            var sb = new StringBuilder();
            sb.AppendLine("You are an expert fitness and nutrition coach. Provide a concise, actionable exercise plan and dietary suggestions tailored to the user's data. Provide beginner/intermediate/advanced variants and note any important safety tips.");
            sb.AppendLine();
            sb.AppendLine("User data:");
            if (profile.HeightCm.HasValue) sb.AppendLine($"- Height: {profile.HeightCm.Value} cm");
            if (profile.WeightKg.HasValue) sb.AppendLine($"- Weight: {profile.WeightKg.Value} kg");
            if (!string.IsNullOrEmpty(profile.BodyType)) sb.AppendLine($"- Body type / note: {profile.BodyType}");
            if (!string.IsNullOrEmpty(profile.ImageUrl))
                sb.AppendLine($"- User uploaded an image at: {profile.ImageUrl} (describe likely physique and suggest appropriate intensity adjustments).");
            sb.AppendLine();
            sb.AppendLine("Return the response as plain text with sections: Summary, Exercise Plan, Nutrition, Safety Notes.");

            var userMessage = sb.ToString();

            var request = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "system", content = "You are a friendly, evidence-based fitness trainer." },
                    new { role = "user", content = userMessage }
                },
                max_tokens = 600 // lower tokens to reduce cost and possible throttling
            };

            string payload = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

            // Retry policy: exponential backoff with jitter, honor Retry-After header when available
            const int maxAttempts = 5;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                HttpResponseMessage res = null!;
                try
                {
                    res = await _http.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);
                }
                catch (Exception ex)
                {
                    // network-level error - retry a few times
                    if (attempt == maxAttempts)
                        return $"AI service network error: {ex.Message}";
                    await Task.Delay(ComputeDelay(attempt));
                    continue;
                }

                // success path
                if (res.IsSuccessStatusCode)
                {
                    var resJson = await res.Content.ReadAsStringAsync();
                    try
                    {
                        using var doc = JsonDocument.Parse(resJson);
                        if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                        {
                            var first = choices[0];
                            if (first.TryGetProperty("message", out var msg) && msg.TryGetProperty("content", out var content))
                                return content.GetString() ?? "No recommendation returned.";
                        }
                        return "AI returned an unexpected payload shape.";
                    }
                    catch (JsonException)
                    {
                        return "Failed to parse response from AI service.";
                    }
                }

                _logger.LogInformation("OpenAI response code: {StatusCode}", (int)res.StatusCode);
                if (res.Headers.TryGetValues("Retry-After", out var raf)) _logger.LogInformation("Retry-After: {RetryAfter}", string.Join(",", raf));
                if (res.Headers.TryGetValues("X-RateLimit-Limit", out var rl)) _logger.LogInformation("RateLimit-Limit: {Limit}", string.Join(",", rl));

                // handle rate limits and server errors
                if (res.StatusCode == (HttpStatusCode)429 || (int)res.StatusCode >= 500)
                {
                    // look for Retry-After
                    if (res.Headers.TryGetValues("Retry-After", out var values))
                    {
                        if (int.TryParse(System.Linq.Enumerable.FirstOrDefault(values), out var seconds))
                        {
                            await Task.Delay(TimeSpan.FromSeconds(seconds));
                            continue;
                        }
                    }
                    // otherwise exponential backoff with jitter
                    if (attempt == maxAttempts)
                    {
                        var body = await SafeReadContent(res);
                        return $"AI service rate limited or unavailable (status {(int)res.StatusCode}). Response: {body}";
                    }
                    await Task.Delay(ComputeDelay(attempt));
                    continue;
                }

                // other client errors - don't retry
                var err = await SafeReadContent(res);
                return $"AI service returned error {(int)res.StatusCode}: {err}";
            }

            return "AI service unavailable after retries. Please try again later.";

            static int ComputeDelay(int attempt)
            {
                // base 500ms, exponential, plus 100-600ms jitter
                var rng = new Random();
                int jitter = rng.Next(100, 600);
                return (int)(Math.Pow(2, attempt) * 500) + jitter;
            }

            static async Task<string> SafeReadContent(HttpResponseMessage message)
            {
                try
                {
                    return await message.Content.ReadAsStringAsync();
                }
                catch
                {
                    return "<no body>";
                }
            }
        }
    }
}