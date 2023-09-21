using Microsoft.AspNetCore.Mvc;
using PomeloAPI.Controllers;
using PomeloAPI.Services;
using PomeloAPI.Models;
using NSubstitute;

namespace PomeloTest;

public class PomeloTest
{
    private readonly PomeloController _repository;
    public PomeloTest()
    {
        var authentication = new ServiceAuthentication();
        var service = new Service_PomeloAPI(authentication);
        _repository = new PomeloController(service);
    }
    
    [Fact]
    public async Task PomeloService_GetUsers_Success()
    {
        
        // Act
        var users = await _repository.Get();

        // Assert
        Assert.NotEmpty(users);
        Assert.IsType<List<UserData>>(users);

    }
    
    [Fact]
    public async Task PomeloService_CreateUser_Success()
    {
        // Arrange
        const string email_created = "testing@mail.com";
        var user = new CreateUserDTO()
        {
            name = "Jonathan",
            surname = "Turro",
            birthdate = "1998-08-20",
            gender = "MALE",
            email = email_created,
            phone = "1123456789",
            nationality = "ARG",
            legal_address = new LegalAddressDTO
            {
                street_name = "Av. Corrientes",
                street_number = 300,
                floor = 1,
                apartment = "A",
                zip_code = 1414,
                neighborhood = "Villa Crespo",
                city = "CABA",
                region = "Buenos Aires",
                additional_info = "Torre 2",
                country = "MEX"
            },
            operation_country = "MEX"
        };
        
        // Act
        
       var createdUser =  await _repository.CreateUser(user);
        

        // Assert
        Assert.IsType<ActionResult<UserData>>(createdUser);
        Assert.Equal(email_created, user.email);
    }

    [Fact]
    public async Task PomeloService_GetUserById_Success()
    {
        //Arrange
        var idToFind = "usr-2ViWCCW1HNfHJJ7RxsOFqweyaYD";
        var nameOfUserExcpected = "Julian";
        
        //Act
        var userFound = await _repository.Get(idToFind);
        
        //Assert

        Assert.IsType<UserData>(userFound);
        Assert.Equal(nameOfUserExcpected, userFound.name);
    }
}