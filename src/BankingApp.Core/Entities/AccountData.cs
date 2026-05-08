namespace BankingApp.Core.Entities;

public class AccountData
{
    public int AccountId { get; set; }
    public decimal Balance { get; set; }
    public decimal Overdraft { get; set; }
    public string Currency { get; set; } = "USD";
}
