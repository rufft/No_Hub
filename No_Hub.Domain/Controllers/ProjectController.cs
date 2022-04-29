using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using No_Hub.Domain.Data;
using No_Hub.Domain.Data.DTOs;
using No_Hub.Domain.Services.Interfaces;

namespace No_Hub.Domain.Controllers;

public class ProjectController : Controller
{
    private readonly DataContext _context;
    private readonly ILogger<ProjectController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IProjectManager _projectManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _dbContext;

    public ProjectController(DataContext context,
        ILogger<ProjectController> logger,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        DataContext dbContext, IProjectManager projectManager)
    {
        _context = context;
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _dbContext = dbContext;
        _projectManager = projectManager;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("{userId}/projects")]
    public async Task<IActionResult> Projects([FromRoute] string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            ViewBag.Error = "Server error";

            _logger.LogError("Authorized user is null");
            
            //TODO: Create view
            return View();
        }
        
        var projects = await _projectManager.GetAllUserProjectsAsync(user.Id);

        //TODO: Create view
        return View(projects);

    }

    [HttpGet]
    [AllowAnonymous]
    [Route("project/{id}")]
    public IActionResult Project([FromRoute] string id)
    {
        return View(id);
    }

    [HttpGet]
    [Authorize]
    [Route("project/create")]
    public IActionResult CreateProject() => View();

    [HttpPost]
    [Authorize]
    [Route("project/create")]
    public async Task<IActionResult> CreateProject(ProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        //TODO: Create new service for checking auth user 
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            ViewBag.Error = "Server error";
            
            _logger.LogError("Authorized user is null");
            return View();
        }
        
        var project = dto.ToProject(user);

        try
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Add project to db: {Message}", e.Message);
            ViewBag.Error = "Server error";
            return View(dto);
        }

        return RedirectToAction("Index");
    }
}