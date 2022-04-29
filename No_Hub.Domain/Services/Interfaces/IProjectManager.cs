using Microsoft.AspNetCore.Identity;
using No_Hub.Domain.Models;

namespace No_Hub.Domain.Services.Interfaces;

public interface IProjectManager
{
    public Task<Project?> GetProjectByIdAsync(string projectId);
    public Task<IEnumerable<Project>?> GetAllUserProjectsAsync(string userId);
}