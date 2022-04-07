using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace studash_service.Models
{
    public class ScheduleRequest : IRequireGroup , IRequireUniversity
    {
        [Required]
        public string University { get; set; }
        [Required]
        public string GroupName { get; set; }
        [DefaultValue("2000-01-01T00:00:00")]
        public DateTime? Date { get; set; }
    }
}