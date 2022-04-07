using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using studash_service.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace studash_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Route("plan")]
        [SwaggerOperation(Summary = "Запрос на получение учебного плана", Description = "Запрашивает из базы данных путь к файлу на удаленном хранилище")]
        [SwaggerResponse(201, "Возвращает файл учебного плана")]
        [SwaggerResponse(204, "Данные не найдены или не правильно составлен запрос")]
        [SwaggerResponse(500, "Произошла ошибка при получении расписания")]
        public ObjectResult GetPlan(PlanRequest request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            try
            {
                var context = _contexts[request.University];
                var plan = context.educational_plans
                                  .Single(plan => plan.speciality_code == request.SpecialityCode);
                var filename = $"educational-plans/{plan.reference}";
                Stream stream = _storage.GetAsStreamAsync(filename).Result;
                return Ok(JsonDocument.Parse(stream));
            }
            catch (KeyNotFoundException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: statusCode, detail: $"Университет не найден");
            }
            catch (InvalidOperationException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: statusCode, detail: $"Неверный код специальности");
            }
            catch (ArgumentNullException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: 500, detail: $"Необходимо указать Университет");
            }
            catch (Exception e)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body}) {e.Message}");
                return Problem(statusCode: 500, detail: $"Internal error");
            }
        }
    }
}