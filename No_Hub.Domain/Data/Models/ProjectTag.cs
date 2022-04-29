using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace No_Hub.Domain.Models;

public class ProjectTag
{
    private ProjectTag() { }
    public ProjectTag(string name, IdentityUser? createdBy = null)
    {
        Name = name;
        CreatedBy = createdBy;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }
    
    public string Name { get; init; }
    
    public IdentityUser? CreatedBy { get; init; }
}