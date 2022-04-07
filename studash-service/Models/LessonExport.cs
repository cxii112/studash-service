using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace studash_service.Models
{
    public class LessonExport
    {
        public LessonExport(Lesson lesson)
        {
            id = lesson.id;
            date = lesson.date;
            group_name = lesson.group_name;
            has_lessons = lesson.has_lessons;
            start_time = lesson.start_time.ToString();
            end_time = lesson.end_time.ToString();
            subject = lesson.subject;
            teacher = lesson.teacher;
            auditorium = lesson.auditorium;
            building = lesson.building;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public DateTime date { get; set; }
        public string group_name { get; set; }
        public bool has_lessons { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string subject { get; set; }
        public string teacher { get; set; }
        public string auditorium { get; set; }
        public string building { get; set; }
    }
}