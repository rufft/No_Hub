using System.ComponentModel.DataAnnotations.Schema;
using No_Hub.Domain.Models.Abstractions;

namespace No_Hub.Domain.Models;

public class MarkDown : IIdentifier
{
    private MarkDown() {}
    
    public MarkDown(string markDownText, IEnumerable<Image> images)
    {
        MarkDownText = markDownText;
        LastUpdate = DateTime.Now;
        Images = images;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }
    
    public string MarkDownText { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public IEnumerable<Image> Images { get; set; }
}