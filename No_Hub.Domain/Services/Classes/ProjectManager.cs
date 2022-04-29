using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using No_Hub.Domain.Data;
using No_Hub.Domain.Models;
using No_Hub.Domain.Services.Interfaces;

namespace No_Hub.Domain.Services.Classes;

public class ProjectManager : IProjectManager
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _dbContext;
    private readonly ILogger<ProjectManager> _logger;

    public ProjectManager(UserManager<IdentityUser> userManager,
        ILogger<ProjectManager> logger,
        DataContext dbContext)
    {
        _userManager = userManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Project?> GetProjectByIdAsync(string projectId) =>
        await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
    

    public async Task<IEnumerable<Project>?> GetAllUserProjectsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return null;
        }

        return _dbContext.Projects
            .Include(p => p.Creators)
            .Where(p => p.Creators.Contains(user));
    }
}