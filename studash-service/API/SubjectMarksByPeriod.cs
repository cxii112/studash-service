namespace studash_service.Models
{
    public class SubjectMarksByPeriod : PeriodMarksRequest, IRequireSubject
    {
        public string Subject { get; set; }
    }
}