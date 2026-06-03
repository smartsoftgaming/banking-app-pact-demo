namespace BankingApp.Application.Contracts;

public sealed class UserResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; } 
    public string Email { get; set; } = default!;
}
