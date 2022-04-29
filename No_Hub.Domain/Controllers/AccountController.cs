using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No_Hub.Domain.Data.DTOs;

namespace No_Hub.Domain.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] UserLoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "There is no such user");
            return View(dto);
        }

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);
        
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid login request");
            return View(dto);
        }
        
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] UserRegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);

        if (existingUser is not null)
        {
            ModelState.AddModelError("", "User with such email already exist");
            return View(dto);
        }

        var user = new IdentityUser
        {
            Email = dto.Email,
            UserName = dto.UserName
        };
        
        var userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "user");

        if (userRole is null)
        {
            _logger.LogError("The user role does not exist!");
            ModelState.AddModelError("", "Failed to register user");
            return View(dto);
        }

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed to register user {User}", user.Email);
            ModelState.AddModelError("", "Failed to register user");
            return View(dto);
        }
        
        await _userManager.AddToRoleAsync(user, userRole.Name);
        await _signInManager.SignInAsync(user, true);

        return RedirectToAction("Index", "Home");
    }
    
}