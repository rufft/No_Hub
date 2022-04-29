namespace No_Hub.Domain.Models;

public class SetupResult
{
    public SetupResult(bool success, params string[] errors)
    {
        Success = success;
        Errors = errors;
    }

    public bool Success { get; }
    public string[] Errors { get; }
}