namespace studash_service.Models
{
    public class YearMarksRequest : AllMarksRequest, IRequireYear
    {
        public int Year { get; set; }
    }
}