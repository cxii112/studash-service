namespace studash_service.Models
{
    public class PeriodMarksRequest : YearMarksRequest, IRequirePeriod
    {
        public int Period { get; set; }
    }
}