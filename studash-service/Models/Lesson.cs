using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace studash_service.Context
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public DateTime date { get; set; }
        public string group_name { get; set; }
        public bool has_lessons { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public string subject { get; set; }
        public string teacher { get; set; }
        public string auditorium { get; set; }
        public string building { get; set; }
    }
}