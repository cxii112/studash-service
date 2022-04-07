using AspNetCore.Yandex.ObjectStorage;
using Microsoft.EntityFrameworkCore;

namespace studash_service.Models
{
    public class EducationalPlanContext : UniversityContext
    {
        public EducationalPlanContext(string key, YandexStorageService storage) : base(key, storage) { }
        public DbSet<EducationalPlan> educational_plans { get; set; }
    }
}