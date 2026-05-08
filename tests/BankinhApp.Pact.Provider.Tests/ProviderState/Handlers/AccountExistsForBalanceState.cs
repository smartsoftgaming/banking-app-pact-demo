using Moq;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace BankinhApp.Pact.Provider.Tests.ProviderState.Handlers;

public class UserExistsState(Mock<IUserRepository> userRepositoryMock) : IProviderStateHandler
{
    public string State => "user exists";

    public Task ApplyAsync()
    {
        userRepositoryMock
            .Setup(r => r.GetById(1))
            .Returns(new UserData
            {
                UserId = 1,
                Username = "giorgi",
                Email = "giorgi@test.com"
            });

        return Task.CompletedTask;
    }
}