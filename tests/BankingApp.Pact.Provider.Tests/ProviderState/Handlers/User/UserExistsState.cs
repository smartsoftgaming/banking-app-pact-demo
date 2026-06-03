using Moq;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace BankingApp.Pact.Provider.Tests.ProviderState.Handlers.User;

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
                UserName = "test",
                Email = "test@test.com"
            });

        return Task.CompletedTask;
    }
}