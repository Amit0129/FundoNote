using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBusiness
    {
        public Task<CollaboratorEntity> AddCollab(AddCollabModel collabModel, long userId, long noteId);
        public Task<bool> DeleteAColab(int colabId, long userID, long noteId);
        public Task<IEnumerable<CollaboratorEntity>> GetAllCollab(long userID, long noteId);
    }
}
