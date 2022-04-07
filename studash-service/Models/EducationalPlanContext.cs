using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.EntityFrameworkCore;

namespace studash_service.Models
{
    public class EducationalPlanContext : DbContext
    {
        private readonly string _databasesFilename = "databases.json";

        public EducationalPlanContext(string key, YandexStorageService storageService)
        {
            var stream = storageService.GetAsStreamAsync(_databasesFilename).Result;
            var fileStreamReader = new StreamReader(stream);
            var json = fileStreamReader.ReadToEnd();
            var connectionsData = JsonSerializer.Deserialize<Dictionary<string, DatabaseConnectionData>>(json);

            ConnectionData = connectionsData[key] ?? throw new ArgumentNullException(nameof(connectionsData));
        }

        private IDatabaseConnectionData ConnectionData { get; set; }
        public DbSet<EducationalPlan> educationalplans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder // Создание строки подключения из данных с удаленного ресурса
                .UseNpgsql($@"Host={ConnectionData.Host};Database={ConnectionData.Database};Username={ConnectionData.Username};Password={ConnectionData.Password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EducationalPlan>()
                .HasKey(e => new { SpecialityCode = e.specialitycode, PlanReference = e.planreference, ID = e.id });
            base.OnModelCreating(modelBuilder);
        }
    }
}