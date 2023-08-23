using CommonLayer.Models;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRepo
    {
        public LabelEntity AddLabel(AddLabelModel addLabel, long userId, long noteId);
    }
}
