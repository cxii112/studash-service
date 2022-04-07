using AspNetCore.Yandex.ObjectStorage;
using Microsoft.EntityFrameworkCore;

namespace studash_service.Models
{
    public class ScheduleContext : UniversityContext
    {
        public ScheduleContext(string key, YandexStorageService storage) : base(key, storage) { }
        public DbSet<Lesson> schedule { get; set; }
    }
}