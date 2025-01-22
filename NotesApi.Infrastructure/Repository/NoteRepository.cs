using Microsoft.EntityFrameworkCore;
using NotesApi.Core.Entities;
using NotesApi.Infrastructure.Context;
using NotesApi.Infrastructure.Repository.Interfaces;

namespace NotesApi.Infrastructure.Repository
{
    public class NoteRepository(NotesDbContext _context) : INoteRepository
    {
        public async Task CreateNoteAsync(Note note)
        {
            await _context.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllNotesAsync(Guid userId)
        {
            return await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Note> GetByIdAsync(Guid id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            return note;
        }

        public async Task UpdateNoteAsync(Guid id, string newDescription)
        {
            var note = await GetByIdAsync(id);
            note.UpdatedAt = DateTime.UtcNow;
            note.Description = newDescription;
            await _context.SaveChangesAsync();
        }
    }
}
