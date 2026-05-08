namespace BankingApp.Application.Contracts;

public sealed class BalanceResponse
{
    public int AccountId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; } = default!;
}