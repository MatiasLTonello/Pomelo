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
        private readonly ServiceAuthentication _authentication;

        public Service_PomeloAPI(ServiceAuthentication authentication)
        {
            _authentication = authentication;
            _token = _authentication.GetAuthenticationToken().Result;

        }


        public async Task<CreatedCard> CreateCard(Card newCard)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(newCard), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/cards/v1", content);

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<CardResponse>(json_response);
                return resultado.data;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Log.Error("Error creating card : " + errorMessage);
                throw new Exception($"La solicitud a la API no fue exitosa. Código de estado HTTP:  {response.StatusCode}. Mensaje: {errorMessage}");
            }
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

            var client = new HttpClient();

            client.BaseAddress = new Uri(_baseurl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.GetAsync("/users/v1/?page[size]=20");

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<GetUsersAPIResponse>(json_response);

                return resultado.data;
            } else
            {
                Log.Error("Error GetUsers " + response);
                throw new Exception("La solicitud a la API no fue exitosa. Código de estado HTTP: " + response.StatusCode);

            }

           
        }
    }
}

