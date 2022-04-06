using System;
using Microsoft.EntityFrameworkCore;

namespace studash_service.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(string key)
        {
            var connectionData = ExternalResourcesLoader.LoadDatabaseData(key);
            ConnectionData = connectionData ?? throw new ArgumentNullException(nameof(connectionData));
        }

        public DbSet<Lesson> schedule { get; set; }
        private IDatabaseConnectionData ConnectionData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder // Создание строки подключения из данных с удаленного ресурса
                .UseNpgsql($@"Host={ConnectionData.Host};Database={ConnectionData.Database};Username={ConnectionData.Username};Password={ConnectionData.Password}");
        }
    }
}