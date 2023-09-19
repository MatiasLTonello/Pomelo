using PomeloAPI.Models;
namespace PomeloAPI.Services

{
	public interface IServicePomeloAPI
	{
        Task <UserData> CreateUser(CreateUserDTO user);
        Task<List<UserData>> GetUsers();
        Task<UserData> GetUser(string id);
        Task<CreatedCard> CreateCard(Card newCard);
        Task<List<CreatedCard>> GetCards();
    }
}

