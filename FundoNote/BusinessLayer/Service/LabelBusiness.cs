using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        public readonly ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }
        public async Task<LabelEntity> AddLabel(AddLabelModel addLabel, long userId, long noteId)
        {
            try
            {
                return await labelRepo.AddLabel(addLabel, userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<LabelEntity> UpdateLabel(long userId, long labelId, string labelName)
        {
            try
            {
                return await labelRepo.UpdateLabel(userId, labelId, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> DeleteLabel(long userId, long labelId)
        {
            try
            {
                return await labelRepo.DeleteLabel(userId, labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<LabelEntity>> GetLabels(long userId)
        {
            try
            {
                return await labelRepo.GetLabels(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<LabelEntity>> GetLabelsByNote(long userId, long noteId)
        {
            try
            {
                return await labelRepo.GetLabelsByNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
