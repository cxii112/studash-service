using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using studash_service.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace studash_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly Dictionary<string, MarksContext> _contexts;
        private readonly ILogger<MarksController> _logger;
        private YandexStorageService _storage;

        public MarksController(ILogger<MarksController> logger,
                               YandexStorageService     storageService)
        {
            _storage = storageService;
            _logger = logger;
            _contexts =
                new Dictionary<string, MarksContext>
                {
                    { "Хогвардс", new MarksContext("Hogwards", _storage) }
                };
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Запрос на получение всех отчетных единиц",
                          Description = "Запрашивает из базы данных все отчетные единицы студента")]
        [SwaggerResponse(201, "Возвращает массив с данными об отчетных единицах")]
        [SwaggerResponse(204, "Данные не найдены или не правильно составлен запрос")]
        [SwaggerResponse(500, "Произошла ошибка при получении отчетных единиц")]
        public ObjectResult GetAllMarks(AllMarksRequest request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            try
            {
                var context = _contexts[request.University];
                var points = context.marks
                                    .Where(point => point.full_name == request.fullName);
                if (!points.Any())
                {
                    int statusCode = 204;
                    _logger.LogError($"{statusCode} {DateTime.UtcNow}");
                    return new ObjectResult(NoContent());
                }

                _logger.LogError($"{200} {DateTime.UtcNow}");
                return Ok(points);
            }
            catch (KeyNotFoundException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: statusCode, detail: $"Университет не найден");
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

        [HttpPost]
        [Route("Year")]
        [SwaggerOperation(Summary = "Запрос на получение отчетных единиц за год",
                          Description = "Запрашивает из базы данных отчетные единицы студента за указанный год")]
        [SwaggerResponse(201, "Возвращает массив с данными об отчетных единицах")]
        [SwaggerResponse(204, "Данные не найдены или не правильно составлен запрос")]
        [SwaggerResponse(500, "Произошла ошибка при получении отчетных единиц")]
        public ObjectResult GetYearMarks(YearMarksRequest request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            try
            {
                var context = _contexts[request.University];
                var points = context.marks
                                    .Where(point => point.full_name == request.fullName &&
                                                    point.year == request.Year);
                if (!points.Any())
                {
                    int statusCode = 204;
                    _logger.LogError($"{statusCode} {DateTime.UtcNow}");
                    return new ObjectResult(NoContent());
                }

                _logger.LogError($"{200} {DateTime.UtcNow}");
                return Ok(points);
            }
            catch (KeyNotFoundException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: statusCode, detail: $"Университет не найден");
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

        [HttpPost]
        [Route("Period")]
        [SwaggerOperation(Summary = "Запрос на получение отчетных единиц в периоде",
                          Description = "Запрашивает из базы данных отчетные единицы студента в указанном периоде")]
        [SwaggerResponse(201, "Возвращает массив с данными об отчетных единицах")]
        [SwaggerResponse(204, "Данные не найдены или не правильно составлен запрос")]
        [SwaggerResponse(500, "Произошла ошибка при получении отчетных единиц")]
        public ObjectResult GetPeriodMarks(PeriodMarksRequest request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            try
            {
                var context = _contexts[request.University];
                var points = context.marks
                                    .Where(point => point.full_name == request.fullName &&
                                                    point.period == request.Period);
                if (!points.Any())
                {
                    int statusCode = 204;
                    _logger.LogError($"{statusCode} {DateTime.UtcNow}");
                    return new ObjectResult(NoContent());
                }

                _logger.LogError($"{200} {DateTime.UtcNow}");
                return Ok(points);
            }
            catch (KeyNotFoundException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: statusCode, detail: $"Университет не найден");
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

        [HttpPost]
        [Route("Subject")]
        [SwaggerOperation(Summary = "Запрос на получение отчетных единиц по предмету одного периода",
                          Description = "Запрашивает из базы данных отчетные единицы студента по одному предмету за указанный период")]
        [SwaggerResponse(201, "Возвращает массив с данными об отчетных единицах")]
        [SwaggerResponse(204, "Данные не найдены или не правильно составлен запрос")]
        [SwaggerResponse(500, "Произошла ошибка при получении отчетных единиц")]
        public ObjectResult GetSubjectMarksByPeriod(SubjectMarksByPeriod request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            try
            {
                var context = _contexts[request.University];
                var points = context.marks
                                    .Where(point => point.full_name == request.fullName &&
                                                    point.period == request.Period &&
                                                    point.subject == request.Subject);
                if (!points.Any())
                {
                    int statusCode = 204;
                    _logger.LogError($"{statusCode} {DateTime.UtcNow}");
                    return new ObjectResult(NoContent());
                }

                _logger.LogError($"{200} {DateTime.UtcNow}");
                return Ok(points);
            }
            catch (KeyNotFoundException)
            {
                int statusCode = 500;
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body})");
                return Problem(statusCode: statusCode, detail: $"Университет не найден");
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