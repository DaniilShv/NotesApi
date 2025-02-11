﻿namespace NotesApi.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
