namespace studash_service.Models
{
    public interface IRequireGroup : IRequireUniversity
    {
        public string GroupName { get; set; }
    }
}