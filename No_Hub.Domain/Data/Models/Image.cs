using System.ComponentModel.DataAnnotations.Schema;
using No_Hub.Domain.Models.Abstractions;

namespace No_Hub.Domain.Models;

public class Image : IIdentifier
{
    private Image() { }
    
    public Image(string title, byte[] imageData)
    {
        Title = title;
        ImageData = imageData;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }

    public string Title { get; set; }
    
    public byte[] ImageData { get; init; }
}