using NotesApi.Core.Entities;
using NotesApi.Infrastructure.Repository.Interfaces;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services;
public class NoteService(INoteRepository _repository) : INoteService
{
    public async Task CreateNoteAsync(Guid userId, string name, string description)
    {
        Note note = new()
        {
            UserId = userId,
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.CreateNoteAsync(note);
    }

    public async Task RemoveNoteAsync(Guid id)
    {
        var note = await _repository.GetByIdAsync(id);
        note.UpdatedAt = DateTime.UtcNow;
        await _repository.RemoveNoteAsync(note);
    }

    public async Task<List<Note>> GetAllNotesAsync(Guid userId)
    {
        return await _repository.GetAllNotesAsync(userId);
    }

    public async Task UpdatedNoteAsync(Guid id, string newDescription)
    {
        await _repository.UpdateNoteAsync(id, newDescription);
    }
}
