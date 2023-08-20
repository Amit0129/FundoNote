using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundoContext context;
        private readonly IConfiguration Iconfiguration;
        public NoteRepo(FundoContext context, IConfiguration iconfiguration)
        {
            this.context = context;
            Iconfiguration = iconfiguration;
        }
        public NotesEntity AddNote(AddNoteModel noteModel,long userId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = noteModel.Title;
                notesEntity.Note = noteModel.Note;
                notesEntity.RemindMe = noteModel.RemindMe;
                notesEntity.Color = noteModel.Color;
                notesEntity.Image = noteModel.Image;
                notesEntity.IsAechive = noteModel.IsAechive;
                notesEntity.IsPin = noteModel.IsPin;
                notesEntity.IsTrash = noteModel.IsTrash;
                notesEntity.Created = DateTime.Now;
                notesEntity.Edited = DateTime.Now;
                notesEntity.UserId = userId;
                context.Notes.Add(notesEntity);
                context.SaveChanges();
                if (notesEntity != null)
                {
                    return notesEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //Update Notes Of a User==============================
        public NotesEntity UpdateNotes(UpdateNoteModel updateNote,long userId)
        {
            try
            {
                NotesEntity note = context.Notes.FirstOrDefault(x=>x.UserId == userId && x.NoteId== updateNote.NoteId);
                if (note!=null)
                {
                    note.Title = updateNote.Title;
                    note.Note = updateNote.Note;
                    note.RemindMe= updateNote.RemindMe;
                    note.Color = updateNote.Color;
                    note.Image = updateNote.Image;
                    note.IsAechive= updateNote.IsAechive;
                    note.IsPin = updateNote.IsPin;
                    note.IsTrash = updateNote.IsTrash;
                    note.Edited = DateTime.Now;
                    //context.Notes.Update(note);
                    context.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete Notes Of a user=====================
        public bool DeleteNote(DeleteNoteModel deleteNote, long userId)
        {
            try
            {
                var note = context.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == deleteNote.NoteId);
                if (note != null)
                {
                    context.Notes.Remove(note);
                    context.SaveChanges();
                    return true;
                }
                return false;
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
                return context.Notes.Where(x => x.UserId == userId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
