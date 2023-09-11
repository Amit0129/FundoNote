using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    /// <summary>
    /// Collaborator Repo Layer
    /// </summary>
    public class CollaboratorRepo : ICollaboratorRepo
    {
        private readonly FundoContext context;
        public CollaboratorRepo(FundoContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Add Collab to note
        /// </summary>
        /// <param name="collabModel">Collab details</param>
        /// <param name="userId">User Id</param>
        /// <param name="noteId">Note Id </param>
        /// <returns>Collab Info</returns>
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Remove a Collab
        /// </summary>
        /// <param name="colabId">Collab Id</param>
        /// <param name="userID">User Id</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>Boolean Value</returns
        public async Task<bool> DeleteAColab(int colabId, long userID, long noteId)
        {
            try
            {
                var colab = await context.Collaborators.FirstOrDefaultAsync(x => x.collaboratorId == colabId && x.UserId == userID && x.NoteId == noteId);
                if (colab != null)
                {
                    context.Remove(colab);
                    await context.SaveChangesAsync(true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Gel All colab info of a note
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>All collab info</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<CollaboratorEntity>> GetAllCollab(long userID, long noteId)
        {
            try
            {
                return await context.Collaborators.Where(x => x.UserId == userID && x.NoteId == noteId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
