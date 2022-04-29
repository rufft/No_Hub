namespace No_Hub.Domain.Models.ViewModels;

public class BadRouteResult 
{
    public BadRouteResult(string url)
    {
        Errors = new();
        Messages = new();
        Url = url;
    }

    public BadRouteResult(string url, List<string> errors, List<string> messages)
    {
        Errors = errors;
        Messages = messages;
        Url = url;
    }
    
    public string Url { get; }
    public string Title { get; set; } = "There is no such path";
    public List<string> Errors { get; set; }
    public List<string> Messages { get; set; }

}