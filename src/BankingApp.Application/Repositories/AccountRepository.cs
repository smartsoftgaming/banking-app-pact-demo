using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace BankingApp.Application.Repositories;

public class AccountRepository : IAccountRepository
{
    public AccountData GetById(int accountId)
    {
        throw new NotImplementedException();
    }
}
