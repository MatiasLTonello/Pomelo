using Microsoft.AspNetCore.Mvc;
using PomeloAPI.Services;
using PomeloAPI.Models;

namespace PomeloAPI.Controllers
{
    [Route("api/[controller]")]
    public class PomeloController : Controller
    {
        private readonly IServicePomeloAPI servicePomeloAPI;

        public PomeloController(IServicePomeloAPI servicePomelo)
        {
            servicePomeloAPI = servicePomelo;
        }

        [HttpGet("/users")]
        public Task<List<UserData>> Get()
        {
            return servicePomeloAPI.GetUsers();
        }

        [HttpPost("/users/create")]
        public async  Task<ActionResult<UserData>> CreateUser([FromBody] CreateUserDTO user)
        { 
            var user2 =  await servicePomeloAPI.CreateUser(user);
            return CreatedAtAction(nameof(Get), new { id = user2.data.id }, user2);
        }

        [HttpGet("/users/{id}")]
        public Task<UserData> Get(string id)
        {
            return servicePomeloAPI.GetUser(id);
        }

        [HttpGet("/cards")]
        public Task<List<CreatedCard>> GetCards()
        {
            return servicePomeloAPI.GetCards();
        }

        [HttpPost("/cards/create")]
        public Task<CreatedCard> CreateCard([FromBody] Card newCard)
        {
            return servicePomeloAPI.CreateCard(newCard);
        }

    }
}

