using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        public Task<LabelEntity> AddLabel(AddLabelModel addLabel, long userId, long noteId);
        public Task<LabelEntity> UpdateLabel(long userId, long labelId, string labelName);
        public Task<bool> DeleteLabel(long userId, long labelId);
        public Task<IEnumerable<LabelEntity>> GetLabels(long userId);
        public Task<IEnumerable<LabelEntity>> GetLabelsByNote(long userId, long noteId);
    }
}
