using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using studash_service.Context;

namespace studash_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private Dictionary<string, UniversityContext> _contexts;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(ILogger<ScheduleController> logger)
        {
            _logger = logger;
            _contexts =
                new Dictionary<string, UniversityContext>
                {
                    { "Хогвардс", new UniversityContext("Hogwards") }
                };
        }

        [HttpPost]
        public IEnumerable<Lesson> GetAll(ScheduleRequest request)
        {
            _logger.Log(LogLevel.Information,$"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} @ {DateTime.UtcNow}");
            var context = _contexts[request.University];
            StatusCode(201);
            return context.schedule
                          .Where(lesson => lesson.group_name == request.GroupName)
                          .ToArray();
        }

        [HttpPost]
        [Route("/{date}")]
        public IEnumerable<Lesson> GetOnDate(ScheduleRequest request, DateTime date)
        {
            _logger.Log(LogLevel.Information,$"[{Request.Method}] {Request.Host.Host}{Request.Path.Value} @ {DateTime.UtcNow}");
            var context = _contexts[request.University];
            StatusCode(201);
            return context.schedule.Where(lesson => lesson.group_name == request.GroupName &&
                                             lesson.date == date)
                   .ToArray();
        }
    }
}