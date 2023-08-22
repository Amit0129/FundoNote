using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public CollaboratorEntity AddCollab(AddCollabModel collabModel, long userId, long noteId)
        {
            try
            {
                CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                collaboratorEntity.UserId = userId;
                collaboratorEntity.NoteId = noteId;
                collaboratorEntity.CollabEmail = collabModel.Email;
                context.Collaborators.Add(collaboratorEntity);
                context.SaveChanges();
                return collaboratorEntity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete Collab
        public bool DeleteAColab(int colabId,long userID,long noteId)
        {
            try
            {
                var colab = context.Collaborators.FirstOrDefault(x => x.collaboratorId == colabId && x.UserId == userID && x.NoteId == noteId);
                if (colab != null)
                {
                    context.Remove(colab);
                    context.SaveChanges(true);
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
        public IEnumerable<CollaboratorEntity> GetAllCollab(long userID, long noteId)
        {
            try
            {
                return context.Collaborators.Where(x => x.UserId == userID && x.NoteId == noteId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}
