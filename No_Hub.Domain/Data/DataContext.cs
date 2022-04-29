using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using No_Hub.Domain.Models;

namespace No_Hub.Domain.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts)
    {
    }
    
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    public DbSet<ProjectTag> ProjectTags { get; set; }

    public DbSet<MarkDown> MarkDowns { get; set; }

    public DbSet<Image> MarkDownImages { get; set; }


}