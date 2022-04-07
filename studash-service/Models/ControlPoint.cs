using System.ComponentModel.DataAnnotations.Schema;

namespace studash_service.Models
{
    public class ControlPoint
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string full_name { get; set; }
        public int year { get; set; }
        public int period { get; set; }
        public string subject { get; set; }
        public string exam_name { get; set; }
        public int current_mark { get; set; }
        public int min_mark { get; set; }
        public int max_mark { get; set; }
    }
}