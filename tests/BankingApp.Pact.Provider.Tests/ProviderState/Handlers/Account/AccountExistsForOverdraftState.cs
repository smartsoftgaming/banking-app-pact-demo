using Moq;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace BankingApp.Pact.Provider.Tests.ProviderState.Handlers.Account;

public sealed class AccountExistsForOverdraftState(Mock<IAccountRepository> accountRepositoryMock) : IProviderStateHandler
{
    public string State => "account 1 exists for overdraft";

    public Task ApplyAsync()
    {
        accountRepositoryMock
            .Setup(r => r.GetById(1))
            .Returns(new AccountData
            {
                AccountId = 1,
                Balance = 1250.75m,
                Overdraft = 300.00m,
                Currency = "USD"
            });

        return Task.CompletedTask;
    }
}

