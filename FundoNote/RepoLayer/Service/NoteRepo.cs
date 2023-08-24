﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundoContext context;
        private readonly IConfiguration Iconfiguration;
        private const string CLOUD_NAME = "dwml60k7f";
        private const string API_KEY = "318991467217643";
        private const string API_SECRET = "N41mBRTMmi8auMdCqgE9WO_sU-s";
        public static Cloudinary cloud;

        public NoteRepo(FundoContext context, IConfiguration iconfiguration)
        {
            this.context = context;
            Iconfiguration = iconfiguration;
        }
        public async Task<NotesEntity> AddNote(AddNoteModel noteModel, long userId)
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
                await context.Notes.AddAsync(notesEntity);
                await context.SaveChangesAsync();
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
        public async Task<NotesEntity> UpdateNotes(UpdateNoteModel updateNote, long userId)
        {
            try
            {
                NotesEntity note =await context.Notes.FirstOrDefaultAsync(x => x.UserId == userId && x.NoteId == updateNote.NoteId);
                if (note != null)
                {
                    note.Title = updateNote.Title;
                    note.Note = updateNote.Note;
                    note.RemindMe = updateNote.RemindMe;
                    note.Color = updateNote.Color;
                    note.Image = updateNote.Image;
                    note.IsAechive = updateNote.IsAechive;
                    note.IsPin = updateNote.IsPin;
                    note.IsTrash = updateNote.IsTrash;
                    note.Edited = DateTime.Now;
                    //context.Notes.Update(note);
                    await context.SaveChangesAsync();
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
        public async Task<bool> DeleteNote(DeleteNoteModel deleteNote, long userId)
        {
            try
            {
                var note =await context.Notes.FirstOrDefaultAsync(x => x.UserId == userId && x.NoteId == deleteNote.NoteId);
                if (note != null)
                {
                    context.Notes.Remove(note);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                return await context.Notes.Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Ispin==========
        public async Task<NotesEntity> IsPin(long noteId, long userId)
        {
            try
            {
                var note = await context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (note.IsPin == true)
                {
                    note.IsPin = false;
                    await context.SaveChangesAsync();
                    return note;
                }
                note.IsPin = true;
                await context.SaveChangesAsync();
                return note;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //IsAchive=====================
        public async Task<NotesEntity> IsAchive(long noteId, long userId)
        {
            try
            {
                var note = await context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (note.IsAechive == true)
                {
                    note.IsAechive = false;
                    await context.SaveChangesAsync();
                    return note;
                }
                note.IsAechive = true;
                await context.SaveChangesAsync();
                return note;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //IsTrash===================
        public async Task<NotesEntity> IsTrash(long noteId, long userId)
        {
            try
            {
                var note = await context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (note.IsTrash == true)
                {
                    note.IsTrash = false;
                    await context.SaveChangesAsync();
                    return note;
                }
                note.IsTrash = true;
                await context.SaveChangesAsync();
                return note;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Color Change Api===================
        public async Task<NotesEntity> Color(long noteId, string color, long userId)
        {
            try
            {
                var note = await context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (note != null)
                {
                    note.Color = color;
                    await context.SaveChangesAsync();
                    return note;
                }
                note.Color = color;
                await context.SaveChangesAsync();
                return note;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Image Api
        public async Task<NotesEntity> UploadImage(long noteid, IFormFile img, long userId)
        {
            try
            {
                var note = await context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteid && x.UserId == userId);
                if (note != null)
                {
                    Account acc = new Account(CLOUD_NAME, API_KEY, API_SECRET);
                    cloud = new Cloudinary(acc);
                    var imagePath = img.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, imagePath)
                    };
                    var uploadresult = cloud.Upload(uploadParams).SecureUrl;
                    note.Image = uploadresult.ToString();
                    context.Notes.Update(note);
                    await context.SaveChangesAsync();
                    return note;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
