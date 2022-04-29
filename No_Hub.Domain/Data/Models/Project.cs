using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using No_Hub.Domain.Models.Abstractions;

namespace No_Hub.Domain.Models;

public class Project : IIdentifier
{
    private Project() { }
    public Project(
        string name,
        string description,
        IdentityUser creator,
        IEnumerable<ProgrammingLanguage> programmingLanguages,
        IEnumerable<ProjectTag> projectTags,
        bool isClosed,
        DateTime creationTime)
    {
        Name = name;
        Description = description;
        Creators = new [] { creator };
        ProgrammingLanguages = programmingLanguages;
        ProjectTags = projectTags;
        IsClosed = isClosed;
        CreationTime = creationTime;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public IEnumerable<IdentityUser> Creators { get; set; }

    public MarkDown MarkDown { get; set; }

    public bool IsClosed { get; set; }
    
    public IEnumerable<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    
    public IEnumerable<ProjectTag> ProjectTags { get; set; }
    
    public DateTime CreationTime { get; init; }
    
    public DateTime UpdateTime { get; set; }
}