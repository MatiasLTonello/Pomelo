using PomeloAPI.Models;
using Newtonsoft.Json;
using System.Text;
using Serilog;
using PomeloAPI.Services;


namespace PomeloFintech.Services
{
    public class TokenSingleton
    {
        private CredentialResult _token;
        private string _baseurl = "https://api-sandbox.pomelo.la";
        private static TokenSingleton instance = null;
        private static readonly object lockObject = new object();

        private TokenSingleton() { }

        public static TokenSingleton Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new TokenSingleton();

                    return instance;
                }
            }
        }

        public async Task<string> GetAccessToken()
        {
            if (_token == null || isTokenExpired())
            {
                await CreateToken();
            }

            return _token?.access_token;
        }

        private bool isTokenExpired()
        {
            if (_token == null || _token.expires_in <= 0)
            {
                return true;
            }

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var createdTime = new DateTimeOffset(_token.created_at).ToUnixTimeSeconds();
            var expirationTime = createdTime + _token.expires_in;

            return currentTime >= expirationTime;
        }

        private async Task CreateToken()
        {
            var url = _baseurl + "/oauth/token";
            var credentials = new Credential()
            {
                client_id = ConfigService.Instance.GetClientID(),
                client_secret = ConfigService.Instance.GetClientSecret(),
                audience = ConfigService.Instance.GetAudience(),
                grant_type = ConfigService.Instance.GetGrantType()
            };

            var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

            var token = await PomeloAPI.Services.Service_PomeloAPI.PomeloFetch<CredentialResult>(url, HttpMethod.Post,
                content);
            _token = token;

            /*
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(_baseurl);

                var credentials = new Credential()
                {
                    client_id = ConfigService.Instance.GetClientID(),
                    client_secret = ConfigService.Instance.GetClientSecret(),
                    audience = ConfigService.Instance.GetAudience(),
                    grant_type = ConfigService.Instance.GetGrantType()
                };

                var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/oauth/token", content);

                if (response.IsSuccessStatusCode)
                {
                    var json_response = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CredentialResult>(json_response);
                    _token = result;
                }
                else
                {
                    Log.Error("Error creating token : " + response);
                    throw new Exception("La solicitud de autenticación no fue exitosa. Código de estado HTTP: " + response);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error creating token : " + ex);
                throw new Exception("Ocurrió un error en la función CreateToken(): " + ex.Message, ex);
            }*/
        }
    }
}
