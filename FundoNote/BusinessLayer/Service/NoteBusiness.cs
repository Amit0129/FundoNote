using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
	public class NoteBusiness : INoteBusiness
	{
		private readonly INoteRepo noteRepo;
		public NoteBusiness(INoteRepo noteRepo)
		{
			this.noteRepo = noteRepo;
		}
        public async Task<NotesEntity> AddNote(AddNoteModel noteModel, long userId)
        {
			try
			{
				return await noteRepo.AddNote(noteModel, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<NotesEntity> UpdateNotes(UpdateNoteModel updateNote, long userId)
        {
			try
			{
				return await noteRepo.UpdateNotes(updateNote, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<bool> DeleteNote(DeleteNoteModel deleteNote, long userId)
        {
			try
			{
				return await noteRepo.DeleteNote(deleteNote, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<IEnumerable<NotesEntity>> GetUserNotes(long userId)
        {
			try
			{
				return await noteRepo.GetUserNotes(userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<NotesEntity> IsPin(long noteId, long userId)
        {
			try
			{
				return await noteRepo.IsPin(noteId, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}

        public async Task<NotesEntity> IsAchive(long noteId, long userId)
        {
			try
			{
				return await noteRepo.IsAchive(noteId, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}

        public async Task<NotesEntity> IsTrash(long noteId, long userId)
        {
			try
			{
				return await noteRepo.IsTrash(noteId, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<NotesEntity> Color(long noteId, string color, long userId)
        {
			try
			{
				return await noteRepo.Color(noteId, color, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<NotesEntity> UploadImage(long noteid, IFormFile img, long userId)
        {
			try
			{
				return await noteRepo.UploadImage(noteid, img, userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
        public async Task<IEnumerable<NotesEntity>> SearchQuery(long userId, string serchvalue)
		{
			try
			{
				return await noteRepo.SearchQuery(userId, serchvalue);
			}
			catch (Exception)
			{

				throw;
			}
		}

    }
}
