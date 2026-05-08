namespace BankingApp.Application.Contracts;

public sealed class UserResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
}
