using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace CloudCustomers.API.Services;

public interface IUserServices
{
    Task<List<User>> GetUsers();
}

public class UserServices : IUserServices
{
    private readonly HttpClient _httpClient;
    private readonly UserApiOptions apiConfig;

    public UserServices(HttpClient httpClient,
        IOptions<UserApiOptions> apiConfig)
    {
        _httpClient = httpClient;
        this.apiConfig = apiConfig.Value;
    }

    public async Task<List<User>> GetUsers()
    {
        var response = await _httpClient.GetAsync(apiConfig.Endpoint);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        var content = response.Content;
        var users = await content.ReadFromJsonAsync<List<User>>();

        return users.ToList();
    }
}

