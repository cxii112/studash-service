namespace studash_service.Models
{
    public interface IRequireSubject : IRequireUniversity
    {
        public string Subject { get; set; }
    }
}