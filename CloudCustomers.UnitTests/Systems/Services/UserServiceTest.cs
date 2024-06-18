using CloudCustomers.API.Config;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using Microsoft.Extensions.Options;
using Moq.Protected;
using System.Net.Http;

namespace CloudCustomers.UnitTests.Systems.Services;

public class UserServiceTest
{
    [Fact]
    public async Task GetUsers_WhenCalled_InvokesHttpGetRequest()
    {
        // Arrange
        var expectedResponse = UserFixture.GetUserTests();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse);
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = "http://example.com/users"
        });
        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new UserServices(httpClient, config);

        // Act
        await sut.GetUsers();

        // Assert
        handlerMock
            .Protected()
            .Verify(
                  "SendAsync",
                  Times.Exactly(1),
                  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                  ItExpr.IsAny<CancellationToken>()
             );
    }

    [Fact]
    public async Task GetUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        // Arrange
        var expectedResponse = UserFixture.GetUserTests();
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse);
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new UserServices(httpClient, config);

        // Act
        var result = await sut.GetUsers();

        // Assert
        // Assert
        handlerMock
            .Protected()
            .Verify(
                  "SendAsync",
                  Times.Exactly(1),
                  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString() == endpoint),
                  ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetUsers_OnSuccess_ReturnListOfUsers()
    {
        // Arrange
        var expectedResponse = UserFixture.GetUserTests();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse);
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = "http://example.com/users"
        });
        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new UserServices(httpClient, config);

        // Act
        var result = await sut.GetUsers();

        // Assert
        result.Should().BeOfType<List<User>>();
    }


    [Fact]
    public async Task GetUsers_OnNotFound_ReturnNull()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = "http://example.com/users"
        });
        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new UserServices(httpClient, config);

        // Act
        var result = await sut.GetUsers();

        // Assert
        result.Should().BeNull();
    }
}
