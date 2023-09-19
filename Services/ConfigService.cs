public class ConfigService
{
    private IConfiguration _configuration;
    private static ConfigService? _instance;
    private static readonly object _lock = new object();

    private ConfigService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        _configuration = builder.Build();
    }

    public static ConfigService Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigService();
                    }
                }
            }
            return _instance;
        }
    }

    public string GetClientID() => _configuration["ApiSettings:client_id"];

    public string GetClientSecret() => _configuration["ApiSettings:client_secret"];
    
    public string GetAudience() => _configuration["ApiSettings:audience"];
  
    public string GetGrantType() => _configuration["ApiSettings:grant_type"];
    
}
