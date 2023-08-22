using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollaboratorBusiness : ICollaboratorBusiness
    {
        private readonly ICollaboratorRepo collaboratorRepo;
        public CollaboratorBusiness(ICollaboratorRepo collaboratorRepo)
        {
              this.collaboratorRepo = collaboratorRepo;
        }
        public CollaboratorEntity AddCollab(AddCollabModel collabModel, long userId, long noteId)
        {
            try
            {
                return collaboratorRepo.AddCollab(collabModel, userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteAColab(int colabId, long userID, long noteId)
        {
            try
            {
                try
                {
                    return collaboratorRepo.DeleteAColab(colabId, userID, noteId);
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
        public IEnumerable<CollaboratorEntity> GetAllCollab(long userID, long noteId)
        {
            try
            {
                return collaboratorRepo.GetAllCollab(userID, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
