using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public NotesEntity IsPin(long noteId, long userId);
        public NotesEntity IsAchive(long noteId, long userId);
        public NotesEntity IsTrash(long noteId, long userId);
        public NotesEntity Color(long noteId, string color, long userId);
        public NotesEntity UploadImage(long noteid, IFormFile img, long userId);
    }
}
