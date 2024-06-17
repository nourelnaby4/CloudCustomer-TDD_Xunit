using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
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
        var httpClient=new HttpClient(handlerMock.Object);
        var sut= new UserServices(httpClient);

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
    public async Task GetUsers_WhenCalled_ReturnListOfUsers()
    {
        // Arrange
        var expectedResponse = UserFixture.GetUserTests();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new UserServices(httpClient);

        // Act
      var result=  await sut.GetUsers();

        // Assert
       result.Should().BeOfType<List<User>>();
    }
}
