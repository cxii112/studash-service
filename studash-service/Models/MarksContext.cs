using AspNetCore.Yandex.ObjectStorage;
using Microsoft.EntityFrameworkCore;

namespace studash_service.Models
{
    public class MarksContext : UniversityContext
    {
        public MarksContext(string key, YandexStorageService storage) : base(key, storage) { }
        public DbSet<ControlPoint> marks { get; set; }
    }
}