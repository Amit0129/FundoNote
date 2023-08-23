using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Service
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundoContext context;
        public LabelRepo(FundoContext context)
        {
            this.context = context;
        }
        public LabelEntity AddLabel(AddLabelModel addLabel,long userId,long noteId)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.LabelName = addLabel.LabelName;
                label.UserId = userId;
                label.NoteId = noteId;
                context.Update(label);
                context.SaveChanges();
                return label;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
