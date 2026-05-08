using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces;

public interface IAccountRepository
{
    AccountData GetById(int accountId);
}
