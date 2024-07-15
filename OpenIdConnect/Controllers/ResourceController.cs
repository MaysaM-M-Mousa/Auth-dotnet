using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenIdConnect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public string GetAdminResource()
    {
        return "This is an Admin only resource";
    }

    [Authorize(Roles = "Manager")]
    [HttpGet("manager")]
    public string GetManagerResource()
    {
        return "This is an Manager only resource";
    }

    [Authorize(Roles = "Admin, Manager, User")]
    [HttpGet("public")]
    public string GetUserResource()
    {
        return "This is a resource for all roles";
    }
}
