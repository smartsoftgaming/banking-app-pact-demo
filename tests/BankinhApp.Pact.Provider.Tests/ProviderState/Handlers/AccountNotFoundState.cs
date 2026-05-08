using Moq;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace BankinhApp.Pact.Provider.Tests.ProviderState.Handlers;

public sealed class AccountNotFoundState(Mock<IAccountRepository> accountRepositoryMock) : IProviderStateHandler
{
    public string State => "account 999 does not exist";

    public Task ApplyAsync()
    {
        accountRepositoryMock
            .Setup(r => r.GetById(999))
            .Returns((AccountData)null);

        return Task.CompletedTask;
    }
}