using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserServices _userServices;

    public UsersController(IUserServices userServices) 
    {
        _userServices=userServices;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userServices.GetUsers();
        return Ok(users);
    }
}
