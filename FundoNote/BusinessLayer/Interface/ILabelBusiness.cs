using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        public LabelEntity AddLabel(AddLabelModel addLabel, long userId, long noteId);
        public IEnumerable<LabelEntity> GetLabels(long userId);
        public IEnumerable<LabelEntity> GetLabelsByNote(long userId, long noteId);
        public bool DeleteLabel(long userId, long noteId, long labelId);
    }
}
