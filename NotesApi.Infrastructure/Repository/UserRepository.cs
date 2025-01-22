using Microsoft.EntityFrameworkCore;
using NotesApi.Core.Entities;
using NotesApi.Infrastructure.Context;
using NotesApi.Infrastructure.Repository.Interfaces;

namespace NotesApi.Infrastructure.Repository;
public class UserRepository(NotesDbContext _context) : IUserRepository
{
    public async Task CreateAccount(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}
