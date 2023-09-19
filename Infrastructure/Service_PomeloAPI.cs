using PomeloAPI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Serilog;

namespace PomeloAPI.Services
{
	public class Service_PomeloAPI : IServicePomeloAPI
	{
        private string _token;
        private string _baseurl = "https://api-sandbox.pomelo.la";
        private static readonly HttpClient _client = new HttpClient();


        public Service_PomeloAPI(ServiceAuthentication authentication)
        {
            _token = authentication.GetAuthenticationToken().Result;
        }

        public static async Task<T> PomeloFetch<T>(string url, HttpMethod method, HttpContent content = null, string token = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var requestMessage = new HttpRequestMessage(method, url);
                if (content != null)
                {
                    requestMessage.Content = content;
                }
                var response = await _client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var json_response = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<T>(json_response);
                    return resultado;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"La solicitud a la API no fue exitosa. Código de estado HTTP: {response.StatusCode}. Mensaje: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error en PomeloFetch: " + ex.Message);
                throw;
            }
        }

        public async Task<CreatedCard> CreateCard(Card newCard)
        {
            var url = _baseurl + "/cards/v1";
            var content = new StringContent(JsonConvert.SerializeObject(newCard), Encoding.UTF8, "application/json");

            var card = await PomeloFetch<CardResponse>(url, HttpMethod.Post, content, _token);
            return card.data;
        }

        public async Task<List<CreatedCard>> GetCards()
        {

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.GetAsync("/cards/v1");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<GetCardsAPIResponse>(json_response);

                return resultado.data;
            }
            else
            {
                Log.Error("error getting cards : " + response);
                throw new Exception("La solicitud a la API no fue exitosa. Código de estado HTTP: " + response.StatusCode);

            }


        }

        public async Task <UserData> CreateUser(CreateUserDTO newUser)
		{

			var client = new HttpClient();

			client.BaseAddress = new Uri(_baseurl);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/users/v1", content);

			if (response.IsSuccessStatusCode)
			{
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<GetUserResponse>(json_response);

                return await GetUser(resultado.data.id);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // todo: this log can be handled by a middleware.
                Log.Error("error creating user" + errorMessage); 
                throw new Exception($"La solicitud a la API no fue exitosa. Código de estado HTTP: {response.StatusCode}. Mensaje: {errorMessage}");
            }

        

		}

        public async Task<UserData> GetUser(string id)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.GetAsync($"/users/v1/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<GetUserResponse>(json_response);

                return resultado.data;
            }
            else
            {
                Log.Error("Error get user by id " + response.Content);
                throw new Exception("Ocurrió un error en la función getUser" + id + response);

            }

          

        }

        public async Task<List<UserData>> GetUsers()
        {

            var url = _baseurl + "/users/v1/?page[size]=40";
            var users = await PomeloFetch<GetUsersAPIResponse>(url, HttpMethod.Get, null, _token);

            return users.data;
            
        }
    }
}

