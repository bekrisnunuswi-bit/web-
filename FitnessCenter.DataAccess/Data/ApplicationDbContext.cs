using FitnessCenter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitnessCenter.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Center> FitnessCenter { get; set; }
        public DbSet<ServiceTrainer> ServiceTrainers { get; set; }
        public DbSet<TrainerAvailability> TrainerAvailabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BodyProfile> BodyProfiles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Center>().HasData(
                new Center()
                {
                    Id = 1,
                    Name = "IronCore",
                    Description = "Türkiye'nin en modern fitness merkezi ile hayalinizdeki vücuda kavuşun. \r\n" +
                    "                Profesyonel eğitmenler, son teknoloji ekipmanlar ve kişiselleştirilmiş programlarla \r\n" +
                    "                fitness yolculuğunuza başlayın.",
                    OpeningTime = TimeSpan.FromHours(7),
                    ClosingTime = TimeSpan.FromHours(23, 59)
                });
            modelBuilder.Entity<Trainer>().HasData(
                new Trainer
                {
                    Id = 1,
                    Name = "Kadir Nasir",
                    Specialty = "HIIT & Cardio",
                    Bio = "Uzmanlık alanı fonksiyonel antrenmanlar ve kardiyo. 10 yıllık deneyim.",
                    ProfileImageUrl = "/images/trainers/kadirNasir.jpg"
                },
                new Trainer
                {
                    Id = 2,
                    Name = "Deniz Yılmaz",
                    Specialty = "Kas Geliştirme & Güçlenme",
                    Bio = "Vücut geliştirme, güçlenme ve beslenme programları. IFBB sertifikalı.",
                    ProfileImageUrl = "/images/trainers/denizYilmaz.jpg"
                },
                new Trainer
                {
                    Id = 3,
                    Name = "Ece Kaya",
                    Specialty = "Yoga & Barre",
                    Bio = "Esneklik, denge ve stres azaltma üzerine yoğunlaşmıştır. Uluslararası Yoga Eğitmeni.",
                    ProfileImageUrl = "/images/trainers/eceKaya.jpg"
                },
                new Trainer
                {
                    Id = 4,
                    Name = "Furkan Demir",
                    Specialty = "Boxing & Fonksiyonel Antrenman",
                    Bio = "Maraton ve triatlon hazırlıklarında uzmanlaşmıştır. Hızlı ve kalıcı sonuçlar.",
                    ProfileImageUrl = "/images/trainers/furkanDemir.jpg"
                },
                new Trainer
                {
                    Id = 5,
                    Name = "Gizem Akın",
                    Specialty = "Pilates & Core Stability",
                    Bio = "Postür düzeltme ve sırt ağrısı tedavisine yönelik mat ve reformer pilates uzmanı.",
                    ProfileImageUrl = "/images/trainers/gizemAkin.jpg"
                },
                new Trainer
                {
                    Id = 6,
                    Name = "Hakan Can",
                    Specialty = "Genel Fitness & Wellness",
                    Bio = "Günlük hareket kalitesini artıran, yüksek yoğunluklu fonksiyonel dersler vermektedir.",
                    ProfileImageUrl = "/images/trainers/hakanCan.jpg"
                }
            );
            modelBuilder.Entity<TrainerAvailability>().HasData(
                new TrainerAvailability { Id = 1, TrainerId = 1, DayOfWeek = DayOfWeek.Monday, StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(22) },
                new TrainerAvailability { Id = 2, TrainerId = 1, DayOfWeek = DayOfWeek.Wednesday, StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(22) },
                new TrainerAvailability { Id = 3, TrainerId = 1, DayOfWeek = DayOfWeek.Friday, StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(22) },

                new TrainerAvailability { Id = 4, TrainerId = 2, DayOfWeek = DayOfWeek.Tuesday, StartTime = TimeSpan.FromHours(15), EndTime = TimeSpan.FromHours(23) },
                new TrainerAvailability { Id = 5, TrainerId = 2, DayOfWeek = DayOfWeek.Thursday, StartTime = TimeSpan.FromHours(15), EndTime = TimeSpan.FromHours(23) },
                new TrainerAvailability { Id = 6, TrainerId = 2, DayOfWeek = DayOfWeek.Saturday, StartTime = TimeSpan.FromHours(15), EndTime = TimeSpan.FromHours(21) },

                new TrainerAvailability { Id = 7, TrainerId = 3, DayOfWeek = DayOfWeek.Monday, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(15) },
                new TrainerAvailability { Id = 8, TrainerId = 3, DayOfWeek = DayOfWeek.Wednesday, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(15) },
                new TrainerAvailability { Id = 9, TrainerId = 3, DayOfWeek = DayOfWeek.Sunday, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(14) },

                new TrainerAvailability { Id = 10, TrainerId = 4, DayOfWeek = DayOfWeek.Monday, StartTime = TimeSpan.FromHours(7), EndTime = TimeSpan.FromHours(12) },
                new TrainerAvailability { Id = 11, TrainerId = 4, DayOfWeek = DayOfWeek.Tuesday, StartTime = TimeSpan.FromHours(7), EndTime = TimeSpan.FromHours(12) },
                new TrainerAvailability { Id = 12, TrainerId = 4, DayOfWeek = DayOfWeek.Wednesday, StartTime = TimeSpan.FromHours(7), EndTime = TimeSpan.FromHours(12) },
                new TrainerAvailability { Id = 13, TrainerId = 4, DayOfWeek = DayOfWeek.Thursday, StartTime = TimeSpan.FromHours(7), EndTime = TimeSpan.FromHours(12) },
                new TrainerAvailability { Id = 14, TrainerId = 4, DayOfWeek = DayOfWeek.Friday, StartTime = TimeSpan.FromHours(7), EndTime = TimeSpan.FromHours(12) },

                new TrainerAvailability { Id = 15, TrainerId = 5, DayOfWeek = DayOfWeek.Monday, StartTime = TimeSpan.FromHours(13), EndTime = TimeSpan.FromHours(20) },
                new TrainerAvailability { Id = 16, TrainerId = 5, DayOfWeek = DayOfWeek.Tuesday, StartTime = TimeSpan.FromHours(13), EndTime = TimeSpan.FromHours(20) },
                new TrainerAvailability { Id = 17, TrainerId = 5, DayOfWeek = DayOfWeek.Wednesday, StartTime = TimeSpan.FromHours(13), EndTime = TimeSpan.FromHours(20) },
                new TrainerAvailability { Id = 18, TrainerId = 5, DayOfWeek = DayOfWeek.Thursday, StartTime = TimeSpan.FromHours(13), EndTime = TimeSpan.FromHours(20) },
                new TrainerAvailability { Id = 19, TrainerId = 5, DayOfWeek = DayOfWeek.Friday, StartTime = TimeSpan.FromHours(13), EndTime = TimeSpan.FromHours(20) },


                new TrainerAvailability { Id = 20, TrainerId = 6, DayOfWeek = DayOfWeek.Monday, StartTime = TimeSpan.FromHours(18), EndTime = TimeSpan.FromHours(23) },
                new TrainerAvailability { Id = 21, TrainerId = 6, DayOfWeek = DayOfWeek.Wednesday, StartTime = TimeSpan.FromHours(18), EndTime = TimeSpan.FromHours(23) },
                new TrainerAvailability { Id = 22, TrainerId = 6, DayOfWeek = DayOfWeek.Friday, StartTime = TimeSpan.FromHours(18), EndTime = TimeSpan.FromHours(23) },
                new TrainerAvailability { Id = 23, TrainerId = 6, DayOfWeek = DayOfWeek.Sunday, StartTime = TimeSpan.FromHours(16), EndTime = TimeSpan.FromHours(22) }
            );
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Açık Alan Serbest Fitness",
                    Description = "Tüm kardiyo ve ağırlık ekipmanlarına sınırsız erişim. Salon kullanım bileti.",
                    DurationMinutes = 60,
                    Fee = 10.00,
                    MaxCapacity = 50,
                    IsActive = true
                },
                // 2. Güç ve Dayanıklılık
                new Service
                {
                    Id = 2,
                    Name = "STRENGTH - Kas Güçlendirme",
                    Description = "Temel kaldırma ve direnç tekniklerine odaklanan, ağırlık merkezli grup dersi.",
                    DurationMinutes = 60,
                    Fee = 25.00,
                    MaxCapacity = 10,
                    IsActive = true
                },
                // 3. Dövüş Sporları
                new Service
                {
                    Id = 3,
                    Name = "BOXING",
                    Description = "Kickboks teknikleri ile yoğun kardiyo ve güç eğitimi. Ekipman temin edilir.",
                    DurationMinutes = 60,
                    Fee = 22.00,
                    MaxCapacity = 10,
                    IsActive = true
                },
                // 4. Yoga
                new Service
                {
                    Id = 4,
                    Name = "Vinyasa Flow Yoga",
                    Description = "Dinamik akış ve nefes kontrolüne odaklanan denge seansı.",
                    DurationMinutes = 60,
                    Fee = 20.00,
                    MaxCapacity = 10,
                    IsActive = true
                },

                // 5. Pilates (Mat)
                new Service
                {
                    Id = 5,
                    Name = "Mat Pilates",
                    Description = "Merkez kuvveti ve esnekliği geliştirmeye odaklanan temel seviye mat dersi.",
                    DurationMinutes = 60,
                    Fee = 18.00,
                    MaxCapacity = 10,
                    IsActive = true
                },
                new Service
                {
                    Id = 6,
                    Name = "BARRE Dersi",
                    Description = "Bale, yoga ve pilates hareketlerinin birleşimiyle kasları uzatma ve tonlama dersi.",
                    DurationMinutes = 60,
                    Fee = 28.00,
                    MaxCapacity = 10,
                    IsActive = true
                },
                new Service
                {
                    Id = 7,
                    Name = "Spa & Hamam Erişimi",
                    Description = "Günlük olarak Hamam, Sauna ve dinlenme odası erişim bileti.",
                    DurationMinutes = 60,
                    Fee = 35.00,
                    MaxCapacity = 10,
                    IsActive = true
                }
            );
            modelBuilder.Entity<ServiceTrainer>()
                .HasKey(st => new { st.ServiceId, st.TrainerId }
            );
            modelBuilder.Entity<ServiceTrainer>()
                .HasOne(st => st.Service)
                .WithMany(s => s.ServiceTrainers)
                .HasForeignKey(st => st.ServiceId
            );
            modelBuilder.Entity<ServiceTrainer>()
                .HasOne(st => st.Trainer)
                .WithMany(t => t.ServiceTrainers)
                .HasForeignKey(st => st.TrainerId
            );
            modelBuilder.Entity<ServiceTrainer>().HasData(
                new ServiceTrainer { ServiceId = 1, TrainerId = 6 },
                new ServiceTrainer { ServiceId = 2, TrainerId = 2 },
                new ServiceTrainer { ServiceId = 3, TrainerId = 4 },
                new ServiceTrainer { ServiceId = 4, TrainerId = 3 },
                new ServiceTrainer { ServiceId = 5, TrainerId = 5 },
                new ServiceTrainer { ServiceId = 6, TrainerId = 5 },
                new ServiceTrainer { ServiceId = 7, TrainerId = 6 },
                new ServiceTrainer { ServiceId = 2, TrainerId = 6 },
                new ServiceTrainer { ServiceId = 3, TrainerId = 1 }
            );
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade
            );
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict
            );

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Trainer)
                .WithMany()
                .HasForeignKey(a => a.TrainerId)
                .OnDelete(DeleteBehavior.Restrict
            );
        }
    }
}
