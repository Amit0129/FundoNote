using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
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
    public class CollaboratorRepo : ICollaboratorRepo
    {
        private readonly FundoContext context;
        public CollaboratorRepo(FundoContext context)
        {
            this.context = context;
        }
        //Add Collab 
        public async Task<CollaboratorEntity> AddCollab(AddCollabModel collabModel, long userId, long noteId)
        {
            try
            {
                CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                collaboratorEntity.UserId = userId;
                collaboratorEntity.NoteId = noteId;
                collaboratorEntity.CollabEmail = collabModel.Email;
                await context.Collaborators.AddAsync(collaboratorEntity);
                await context.SaveChangesAsync();
                return collaboratorEntity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete Collab
        public async Task<bool> DeleteAColab(int colabId, long userID, long noteId)
        {
            try
            {
                var colab =await context.Collaborators.FirstOrDefaultAsync(x => x.collaboratorId == colabId && x.UserId == userID && x.NoteId == noteId);
                if (colab != null)
                {
                    context.Remove(colab);
                    await context.SaveChangesAsync(true);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get All Colab Email 
        public async Task<IEnumerable<CollaboratorEntity>> GetAllCollab(long userID, long noteId)
        {
            try
            {
                return await context.Collaborators.Where(x => x.UserId == userID && x.NoteId == noteId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
