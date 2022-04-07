namespace studash_service.Models
{
    public interface IRequireSpecialityCode : IRequireUniversity
    {
        public string SpecialityCode { get; set; }
    }
}