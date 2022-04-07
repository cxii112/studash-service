using System.ComponentModel.DataAnnotations;

namespace studash_service.Models
{
    public class AllMarksRequest : IRequireUniversity,IRequireGroup, IRequireStudent
    {
        [Required]
        public string University { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public string fullName { get; set; }
    }
}