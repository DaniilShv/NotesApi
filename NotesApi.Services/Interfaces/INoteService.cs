using NotesApi.Core.Entities;

namespace NotesApi.Services.Interfaces;

public interface INoteService
{
    Task CreateNoteAsync(Guid userId, string name, string description);
    Task UpdatedNoteAsync(Guid id, string newDescription);
    Task<List<Note>> GetAllNotesAsync(Guid userId);
}
