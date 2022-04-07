namespace studash_service.Models
{
    public class PeriodMarksRequest : IRequireStudent, IRequirePeriod
    {
        public int Period { get; set; }
        public string fullName { get; set; }
        public string University { get; set; }
        public string GroupName { get; set; }
    }
}