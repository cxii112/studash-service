using System.ComponentModel.DataAnnotations;

namespace studash_service.Models
{
    public class PlanRequest : IRequireUniversity, IRequireSpecialityCode
    {
        [Required]
        public string University { get; set; }
        [Required]
        public string SpecialityCode { get; set; }
    }
}