using Microsoft.AspNetCore.Identity;
using No_Hub.Domain.Models;

namespace No_Hub.Domain.Services.Interfaces;

public interface ISetupManagerService
{
    public Task<SetupResult> CreateRoleAsync(string roleName);
    public Task<SetupResult> DeleteRoleAsync(string roleName);

    public Task<SetupResult> ChangeUserRoleAsync(IdentityUser user, string roleName, bool isInRole);
}