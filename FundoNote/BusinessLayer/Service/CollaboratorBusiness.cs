using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CollaboratorBusiness : ICollaboratorBusiness
    {
        private readonly ICollaboratorRepo collaboratorRepo;
        public CollaboratorBusiness(ICollaboratorRepo collaboratorRepo)
        {
            this.collaboratorRepo = collaboratorRepo;
        }
        public async Task<CollaboratorEntity> AddCollab(AddCollabModel collabModel, long userId, long noteId)
        {
            try
            {
                return await collaboratorRepo.AddCollab(collabModel, userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> DeleteAColab(int colabId, long userID, long noteId)
        {
            try
            {
                try
                {
                    return await collaboratorRepo.DeleteAColab(colabId, userID, noteId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<CollaboratorEntity>> GetAllCollab(long userID, long noteId)
        {
            try
            {
                return await collaboratorRepo.GetAllCollab(userID, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
