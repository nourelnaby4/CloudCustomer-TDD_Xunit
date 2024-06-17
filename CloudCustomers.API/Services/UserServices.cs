using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public interface IUserServices
{
    Task<List<User>> GetUsers();
}

public class UserServices : IUserServices
{
    private readonly HttpClient _httpClient;

    public UserServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetUsers()
    {
        var usersResponse = await _httpClient.GetAsync("https://example.com");

        return new List<User> { };
    }
}
