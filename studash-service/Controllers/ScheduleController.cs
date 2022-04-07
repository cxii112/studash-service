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
    public class ScheduleController : ControllerBase
    {
        private readonly Dictionary<string, ScheduleContext> _contexts;
        private readonly ILogger<ScheduleController> _logger;
        private YandexStorageService _storage;

        public ScheduleController(ILogger<ScheduleController> logger, YandexStorageService storageService)
        {
            _storage = storageService;
            _logger = logger;
            _contexts =
                new Dictionary<string, ScheduleContext>
                {
                    { "Хогвардс", new ScheduleContext("Hogwards", _storage) }
                };
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Формирует полное расписание", Description = "Запрашивает из базы данных расписание группы")]
        [SwaggerResponse(201, "Возвращает массив с расписанием")]
        [SwaggerResponse(204, "Данные не найдены или не правильно составлен запрос")]
        [SwaggerResponse(500, "Произошла ошибка при получении расписания")]
        public ObjectResult GetAll(ScheduleRequest request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            ScheduleContext context;
            try
            {
                context = _contexts[request.University];
                var schedule = context.schedule.Where(lesson => lesson.group_name == request.GroupName)
                                      .ToArray();
                if (schedule.Count() == 0)
                {
                    int statusCode = 204;
                    _logger.LogError($"{statusCode} {DateTime.UtcNow}");
                    return new ObjectResult(NoContent());
                };
                _logger.LogError($"{200} {DateTime.UtcNow}");
                return Ok(schedule);
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
        [Route("onDate")]
        [SwaggerOperation(Summary = "Формирует расписание", Description = "Запрашивает из базы данных расписание группы на указанную дату")]
        [SwaggerResponse(201, "Возвращает массив с расписанием")]
        [SwaggerResponse(204, "Возвращает пустой массив")]
        [SwaggerResponse(500, "Произошла ошибка при получении расписания")]
        public ObjectResult GetOnDate(ScheduleRequest request)
        {
            _logger.LogInformation($"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} {DateTime.UtcNow}");
            ScheduleContext context;
            try
            {
                context = _contexts[request.University];
                var schedule = context.schedule.Where(lesson => lesson.group_name == request.GroupName &&
                                                            lesson.date == request.Date)
                                  .ToArray();
                if (schedule.Count() == 0)
                {
                    int statusCode = 204;
                    _logger.LogError($"{statusCode} {DateTime.UtcNow}");
                    return new ObjectResult(NoContent());
                };
                _logger.LogError($"{200} {DateTime.UtcNow}");
                return Ok(schedule);
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
                _logger.LogError($"{statusCode} {DateTime.UtcNow} ({Request.Body}) {e.Message} {e.Data}");
                return Problem(statusCode: 500, detail: $"Internal error");
            }
        }
    }
}