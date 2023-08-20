using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRepo
    {
        public NotesEntity AddNote(AddNoteModel noteModel, long userId);
        public NotesEntity UpdateNotes(UpdateNoteModel updateNote, long userId);
        public bool DeleteNote(DeleteNoteModel deleteNote, long userId);
        public IEnumerable<NotesEntity> GetUserNotes(long userId);
    }
}
