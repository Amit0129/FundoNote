using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INoteRepo
    {
        public Task<NotesEntity> AddNote(AddNoteModel noteModel, long userId);
        public Task<NotesEntity> UpdateNotes(UpdateNoteModel updateNote, long userId);
        public Task<bool> DeleteNote(DeleteNoteModel deleteNote, long userId);
        public Task<IEnumerable<NotesEntity>> GetUserNotes(long userId);
        public Task<NotesEntity> IsPin(long noteId, long userId);
        public Task<NotesEntity> IsAchive(long noteId, long userId);
        public Task<NotesEntity> IsTrash(long noteId, long userId);
        public Task<NotesEntity> Color(long noteId, string color, long userId);
        public Task<NotesEntity> UploadImage(long noteid, IFormFile img, long userId);
    }
}
