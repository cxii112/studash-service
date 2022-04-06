using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using studash_service.Context;

namespace studash_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private Dictionary<string, UniversityContext> _contexts;

        public ScheduleController()
        {
            _contexts =
                new Dictionary<string, UniversityContext>
                {
                    { "Хогвардс", new UniversityContext("Hogwards") }
                };
        }

        [HttpPost]
        [Route("/All")]
        public IEnumerable<Lesson> GetAll(FullScheduleRequest request)
        {
            var context = _contexts[request.University];
            return context.schedule
                          .Where(lesson => lesson.group_name == request.GroupName)
                          .ToArray();
        }
    }
}