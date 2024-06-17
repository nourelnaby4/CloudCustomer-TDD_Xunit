using CloudCustomers.API.Models;
using System.Net;

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
        var response = await _httpClient.GetAsync("https://example.com");

        if(response.StatusCode == HttpStatusCode.NotFound)
            return null;

        var content =  response.Content;
        var users = await content.ReadFromJsonAsync<List<User>>();

        return users.ToList();
    }
}
