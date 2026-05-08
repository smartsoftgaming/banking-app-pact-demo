using Moq;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace BankinhApp.Pact.Provider.Tests.ProviderState.Handlers;

public sealed class AccountExistsForBalanceState : IProviderStateHandler
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;

    public AccountExistsForBalanceState(Mock<IAccountRepository> accountRepositoryMock)
    {
        _accountRepositoryMock = accountRepositoryMock;
    }

    public string State => "account 1 exists for balance";

    public Task ApplyAsync()
    {
        _accountRepositoryMock
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
