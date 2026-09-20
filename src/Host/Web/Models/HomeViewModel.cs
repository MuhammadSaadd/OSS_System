namespace Web.Models;

public sealed class HomeViewModel
{
    public required string Title { get; init; }
    public required bool DatabaseConnected { get; init; }
    public string? DatabaseStatus { get; init; }
}
