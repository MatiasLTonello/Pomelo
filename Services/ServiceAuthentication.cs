using System;
using System.Threading.Tasks;
using PomeloFintech.Services;
using Serilog;

namespace PomeloAPI.Services
{
    public class ServiceAuthentication
    {
        public async Task<string> GetAuthenticationToken()
        {
            try
            {
                string token = await TokenSingleton.Instance.GetAccessToken();
                return token;
            }
            catch (Exception ex)
            {
                Log.Error("Error getting authentication token : " + ex.Message);
                throw new Exception("Ocurrió un error en la función GetAuthenticationToken(): " + ex.Message, ex);
            }
        }
    }
}
