using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces;

public interface IUserRepository
{
    UserData GetById(int userId);
}
