using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        public readonly ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }
        public LabelEntity AddLabel(AddLabelModel addLabel, long userId, long noteId)
        {
            try
            {
                return labelRepo.AddLabel(addLabel, userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                return labelRepo.GetLabels(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetLabelsByNote(long userId, long noteId)
        {
            try
            {
                return labelRepo.GetLabelsByNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteLabel(long userId, long noteId, long labelId)
        {
            try
            {
                return labelRepo.DeleteLabel(userId, noteId, labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
