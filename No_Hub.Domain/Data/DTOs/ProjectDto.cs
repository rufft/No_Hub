using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using No_Hub.Domain.Models;

namespace No_Hub.Domain.Data.DTOs;

public class ProjectDto
{
    [Required]
    [StringLength(15, MinimumLength = 3)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 10)]
    public string Description { get; set; }
    
    [Required]
    public bool IsClosed { get; set; }

    public IEnumerable<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    
    public IEnumerable<ProjectTag> ProjectTags { get; set; }

    public Project ToProject(IdentityUser user) => 
        new(Name, Description, user, ProgrammingLanguages, ProjectTags, IsClosed, DateTime.Now);
}