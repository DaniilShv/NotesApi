using Identity.PasswordHasher;
using NotesApi.Core.Entities;
using NotesApi.Infrastructure;
using NotesApi.Infrastructure.Repository.Interfaces;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services
{
    public class UserService(IUserRepository _repository) : IUserService
    {
        static PasswordHasher hasher = new PasswordHasher();
        public async Task CreateAccount(string email, string password, string nickname)
        {
            User user = new()
            {
                Email = email,
                Nickname = nickname,
                PasswordHash = hasher.HashPassword(password),
                Notes = new List<Note>()
            };
            await _repository.CreateAccount(user);
        }

        public async Task<User?> ValidateUser(string email, string password)
        {
            var user = await _repository.GetByEmail(email);
            if (user != null)
            {
                if (hasher.VerifyHashedPassword(user.PasswordHash, password))
                    return user;
                else
                    return null;
            }
            return null;
        }
    }
}
