using System.ComponentModel.DataAnnotations.Schema;
using No_Hub.Domain.Models.Abstractions;

namespace No_Hub.Domain.Models;

public class ProgrammingLanguage : IIdentifier
{
    private ProgrammingLanguage() { }
    public ProgrammingLanguage(string name)
    {
        Name = name;
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }
    
    public string Name { get; init; }
}