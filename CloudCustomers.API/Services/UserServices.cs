using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public interface IUserServices
{
    Task<List<User>> GetUsers();
}

public class UserServices : IUserServices
{
    public Task<List<User>> GetUsers()
    {
        throw new NotImplementedException();
    }
}
