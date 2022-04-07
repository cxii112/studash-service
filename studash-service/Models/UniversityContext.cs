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
        private readonly string _databasesFilename = "databases.json";

        public UniversityContext(string key, YandexStorageService storage)
        {
            var stream = storage.GetAsStreamAsync(_databasesFilename).Result;
            var fileStreamReader = new StreamReader(stream);
            var json = fileStreamReader.ReadToEnd();
            var connectionsData = JsonSerializer.Deserialize<Dictionary<string, DatabaseConnectionData>>(json);
            
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