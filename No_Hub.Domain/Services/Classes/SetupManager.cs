using Microsoft.AspNetCore.Identity;
using No_Hub.Domain.Models;
using No_Hub.Domain.Services.Interfaces;

namespace No_Hub.Domain.Services.Classes;

public class SetupManager : ISetupManagerService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<SetupManager> _logger;

    public SetupManager(RoleManager<IdentityRole> roleManager,
        ILogger<SetupManager> logger, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<SetupResult> CreateRoleAsync(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return new(false, "Role name can't be empty");
        }

        roleName = roleName.ToLower().Trim();

        var result = await _roleManager.CreateAsync(new(roleName));

        if (result.Succeeded) return new(true);
        
        var errors = result.Errors.Select(e => e.Description).ToArray();
        return new(false, errors);

    }
    
    public async Task<SetupResult> DeleteRoleAsync(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            _logger.LogWarning("Role name can't be empty");
            return new(false, "Role name can't be empty");
        }

        var role = await _roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            _logger.LogWarning("Role {RoleName} doesn't exist", roleName);
            return new(false, "Role doesn't exist");
        }

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded) return new(true);
        
        _logger.LogError("Unable to delete role {RoleName}", roleName);
        var errors = result.Errors.Select(e => e.Description).ToArray();
        foreach (var error in errors)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogError(error);
        }
        return new(false, errors);

    }

    public async Task<SetupResult> ChangeUserRoleAsync(IdentityUser user, string roleName, bool isInRole)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            _logger.LogError("User name or role name is empty!");
            return new(false, "Something goes wrong");
        }

        if (isInRole)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded) return new(true);
            
            _logger.LogError("Unable to remove {UserName} from role {RoleName}", roleName, user.UserName);
            var errors = result.Errors.Select(e => e.Description).ToArray();
            foreach (var error in errors)
            {
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogError(error);
            }
            return new(false, errors);

        }
        else
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded) return new(true);
            
            _logger.LogError("Unable to add {UserName} to role {RoleName}", roleName, user.UserName);
            var errors = result.Errors.Select(e => e.Description).ToArray();
            foreach (var error in errors)
            {
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogError(error);
            }
            return new(false, errors);

        }
        
    }
}