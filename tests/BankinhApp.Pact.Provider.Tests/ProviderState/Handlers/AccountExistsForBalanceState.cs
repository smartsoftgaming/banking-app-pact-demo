using Moq;
using BankinhApp.Pact.Provider.Tests.ProviderState;
using BankingApp.Core.Interfaces;
using BankingApp.Core.Entities;

namespace AccountService.Pact.Provider.Tests.ProviderState.Handlers;

public sealed class UserExistsState : IProviderStateHandler
{
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserExistsState(Mock<IUserRepository> userRepositoryMock)
    {
        _userRepositoryMock = userRepositoryMock;
    }

    public string State => "user exists";

    public Task ApplyAsync()
    {
        _userRepositoryMock
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