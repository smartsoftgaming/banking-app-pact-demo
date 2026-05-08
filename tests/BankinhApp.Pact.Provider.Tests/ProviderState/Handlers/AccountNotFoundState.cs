using Moq;
using BankinhApp.Pact.Provider.Tests.ProviderState;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace AccountService.Pact.Provider.Tests.ProviderState.Handlers;

public sealed class AccountNotFoundState : IProviderStateHandler
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;

    public AccountNotFoundState(Mock<IAccountRepository> accountRepositoryMock)
    {
        _accountRepositoryMock = accountRepositoryMock;
    }

    public string State => "account 999 does not exist";

    public Task ApplyAsync()
    {
        _accountRepositoryMock
            .Setup(r => r.GetById(999))
            .Returns((AccountData?)null);

        return Task.CompletedTask;
    }
}