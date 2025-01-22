using NotesApi.Core.Entities;

namespace NotesApi.Services.Interfaces;
public interface IUserService
{
    Task CreateAccount(string email, string password, string nickname);
    Task<User?> ValidateUser(string email, string password);
}
