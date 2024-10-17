using Npgsql;

namespace TheCrudApp.Database;

public class DatabaseConfig
{    
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }
    
    public string ConnectionString
    {
        get
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Host,
                Port = Port,
                Username = User,
                Password = Password,
                Database = Database,
#if DEBUG
                IncludeErrorDetail = true
#endif 
            };

            return builder.ToString();
        }
    }
}