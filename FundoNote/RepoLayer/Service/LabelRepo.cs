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
    public class LabelRepo : ILabelRepo
    {
        private readonly FundoContext context;
        public LabelRepo(FundoContext context)
        {
            this.context = context;
        }
        //Add Label API
        public LabelEntity AddLabel(AddLabelModel addLabel, long userId, long noteId)
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
        //Get All Label For User
        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                return context.Labels.Where(x => x.UserId == userId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get All Label For A node
        public IEnumerable<LabelEntity> GetLabelsByNote(long userId, long noteId)
        {
            try
            {
                return context.Labels.Where(x => x.UserId == userId && x.NoteId == noteId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete Label
        public bool DeleteLabel(long userId, long noteId, long labelId)
        {
            try
            {
                var label = context.Labels.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.LabelId == labelId);
                if (label != null)
                {
                    context.Labels.Remove(label);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
