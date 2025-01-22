using NotesApi.Core.Entities;

namespace NotesApi.Infrastructure.Repository.Interfaces;

public interface IUserRepository
{
    Task CreateAccount(User user);
    Task<User> GetByEmail(string email);
}
