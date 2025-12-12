using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FitnessCenter.Models;

namespace FitnessCenter.Utility
{
        public static class TimeManagerService
        {
            public static List<TimeSpan> CalculateAvailableSlots(
                IEnumerable<TrainerAvailability> generalAvailability,
                IEnumerable<Appointment> existingAppointments,
                int requiredDurationMinutes,
                TimeSpan centerCloseTime)
            {
                var availableSlots = new List<TimeSpan>();
                TimeSpan requiredDuration = TimeSpan.FromMinutes(requiredDurationMinutes);

                // 1. Genel Müsaitlik Aralıklarında Döngü
                foreach (var slot in generalAvailability.OrderBy(a => a.StartTime))
                {
                    TimeSpan currentSlotTime = slot.StartTime;

                    // Müsaitlik aralığı ve merkez kapanış saati bitene kadar döngü
                    while (currentSlotTime + requiredDuration <= slot.EndTime &&
                           currentSlotTime + requiredDuration <= centerCloseTime)
                    {
                        TimeSpan potentialEndTime = currentSlotTime + requiredDuration;
                        bool isConflicting = false;

                        // 2. Mevcut Randevularla Çakışma Kontrolü
                        foreach (var existingApp in existingAppointments)
                        {
                            // Çakışma Mantığı: Yeni randevunun başlangıcı, mevcut randevunun bitişinden önceyse
                            // VEYA yeni randevunun bitişi, mevcut randevunun başlangıcından sonraysa
                            if (currentSlotTime < existingApp.EndTime && potentialEndTime > existingApp.StartTime)
                            {
                                isConflicting = true;

                                // Çakışma varsa, mevcut randevunun bittiği saatten (EndTime) devam etmeliyiz.
                                currentSlotTime = existingApp.EndTime;
                                break;
                            }
                        }

                        if (!isConflicting)
                        {
                            // Çakışma Yok: Boş slot bulundu.
                            availableSlots.Add(currentSlotTime);
                            // Bir sonraki slotu aramak için randevu süresi kadar ilerle
                            currentSlotTime = potentialEndTime;
                        }
                    }
                }

                // Sadece güncel zamandan sonraki slotları döndür (Bugün için gereklidir)
                if (DateTime.Today.Date == DateTime.Now.Date)
                {
                    TimeSpan now = DateTime.Now.TimeOfDay;
                    availableSlots = availableSlots.Where(t => t > now).ToList();
                }

                return availableSlots;
            }
        }
    }
