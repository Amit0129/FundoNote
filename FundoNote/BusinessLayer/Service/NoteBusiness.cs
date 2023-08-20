using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBusiness : INoteBusiness
    {
		private readonly INoteRepo noteRepo;
		public NoteBusiness(INoteRepo noteRepo)
		{
			this.noteRepo = noteRepo;
		}
        public NotesEntity AddNote(AddNoteModel noteModel, long userId)
        {
			try
			{
				return noteRepo.AddNote(noteModel, userId);
			}
			catch (Exception)
			{

				throw;
			}
        }
        public NotesEntity UpdateNotes(UpdateNoteModel updateNote, long userId)
        {
			try
			{
				return noteRepo.UpdateNotes(updateNote, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public bool DeleteNote(DeleteNoteModel deleteNote, long userId)
		{
			try
			{
				return noteRepo.DeleteNote(deleteNote, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public IEnumerable<NotesEntity> GetUserNotes(long userId)
		{
			try
			{
				return noteRepo.GetUserNotes(userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
