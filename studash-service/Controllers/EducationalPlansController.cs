using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using studash_service.Models;

namespace studash_service.Controllers
{
    [ApiController]
    [Route("plan")]
    public class EducationalPlansController : ControllerBase
    {
        private readonly Dictionary<string, EducationalPlanContext> _contexts;
        private readonly ILogger<EducationalPlansController> _logger;
        private YandexStorageService _storage;

        public EducationalPlansController(ILogger<EducationalPlansController> logger, 
                                          YandexStorageService storageService)
        {
            _storage = storageService;
            _logger = logger;
            _contexts =
                new Dictionary<string, EducationalPlanContext>
                {
                    { "Хогвардс", new EducationalPlanContext("Hogwards", _storage) }
                };
        }

        [HttpPost]
        [Route("{specialityCode}")]
        public JsonDocument GetPlan(PlanRequest request, string specialityCode)
        {
            _logger.Log(LogLevel.Information,$"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} @ {DateTime.UtcNow}");
            var context = _contexts[request.Univercity];
            var plan = context.educationalplans
                              .Single(plan => plan.specialitycode == specialityCode);
            Stream stream = _storage.GetAsStreamAsync(plan.planreference).Result;
            return JsonDocument.Parse(stream);
        }
    }
}