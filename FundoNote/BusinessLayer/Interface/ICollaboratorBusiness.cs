using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBusiness
    {
        public CollaboratorEntity AddCollab(AddCollabModel collabModel, long userId, long noteId);
        public bool DeleteAColab(int colabId, long userID, long noteId);
        public IEnumerable<CollaboratorEntity> GetAllCollab(long userID, long noteId);
    }
}
