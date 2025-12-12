using FitnessCenter.Models;

namespace FitnessCenter.Areas.User.Services
{
    public interface IAIService
    {
        Task<string> GenerateRecommendationAsync(BodyProfile profile);
    }
}
