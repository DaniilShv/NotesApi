using NotesApi.Core.Entities;

namespace NotesApi.Infrastructure.Repository.Interfaces
{
    public interface INoteRepository
    {
        Task CreateNoteAsync(Note note);
        Task UpdateNoteAsync(Guid id, string newDescription);
        Task<Note> GetByIdAsync(Guid id);
        Task<List<Note>> GetAllNotesAsync(Guid userId);
    }
}
