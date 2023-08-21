using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public NotesEntity IsPin(long noteId, long userId)
		{
			try
			{
				return noteRepo.IsPin(noteId, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
		
        public NotesEntity IsAchive(long noteId, long userId)
		{
			try
			{
				return noteRepo.IsAchive(noteId, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
		
        public NotesEntity IsTrash(long noteId, long userId)
		{
			try
			{
				return noteRepo.IsTrash(noteId, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public NotesEntity Color(long noteId, string color, long userId)
		{
			try
			{
				return noteRepo.Color(noteId,color, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public NotesEntity UploadImage(long noteid, IFormFile img, long userId)
        {
			try
			{
				return noteRepo.UploadImage(noteid, img,userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
