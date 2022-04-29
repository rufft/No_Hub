using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using No_Hub.Domain.Models.ViewModels;

namespace No_Hub.Domain.Controllers;

public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [Route("user/{userId}")]
    public async Task<IActionResult> Index([FromRoute] string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user is null 
            ? View("BadRequest", new BadRouteResult("There is no such user")) 
            : View(user);
    }
    
    
}