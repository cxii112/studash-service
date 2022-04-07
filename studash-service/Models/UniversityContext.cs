using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.EntityFrameworkCore;

namespace studash_service.Models
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(string key, YandexStorageService storage)
        {
            var fileStreamReader = new StreamReader(
                                                    storage.GetAsStreamAsync("databases.json").Result);
            var connectionsData = JsonSerializer.Deserialize<Dictionary<string, DatabaseConnectionData>>(
             fileStreamReader.ReadToEnd());
            
            ConnectionData = connectionsData[key] ?? throw new ArgumentNullException(nameof(connectionsData));
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