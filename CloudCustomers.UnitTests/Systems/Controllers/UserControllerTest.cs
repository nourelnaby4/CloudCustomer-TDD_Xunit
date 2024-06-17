using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UserControllerTest
{
    private readonly Mock<IUserServices> _userServicesMock;

    public UserControllerTest()
    {
        _userServicesMock = new();
    }

    [Fact]
    public async Task Get_OnSeccess_Return_StatusCode200()
    {
        // Arrange
        var sut = new UsersController(_userServicesMock.Object);
        _userServicesMock
            .Setup(service => service.GetUsers())
            .ReturnsAsync(UserFixture.GetUserTests());

        // Act
        var result = (OkObjectResult)await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokGetUserExcatlyOnce()
    {
        var sut = new UsersController(_userServicesMock.Object);
        _userServicesMock
            .Setup(service => service.GetUsers())
            .ReturnsAsync(UserFixture.GetUserTests());

        var result = await sut.Get();

        _userServicesMock.Verify(service => service.GetUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        // Arrange
        var sut = new UsersController(_userServicesMock.Object);

        // Act
        _userServicesMock
            .Setup(service => service.GetUsers())
            .ReturnsAsync(UserFixture.GetUserTests());

        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var resultObject = (OkObjectResult)result;
        resultObject.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNotFound_ReturnStatusCode404()
    {
        // Arrange
        var sut = new UsersController(_userServicesMock.Object);

        // Act
        _userServicesMock
            .Setup(service => service.GetUsers())
            .ReturnsAsync(new List<User>());

        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var resultObject = (NotFoundResult)result;
        resultObject.StatusCode.Should().Be(404);
    }
}