namespace studash_service.Context
{
    public class DatabaseConnectionData : IDatabaseConnectionData
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    
    public interface IDatabaseConnectionData
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}