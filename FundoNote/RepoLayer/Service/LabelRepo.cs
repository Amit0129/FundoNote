using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<LabelEntity> AddLabel(AddLabelModel addLabel, long userId, long noteId)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.LabelName = addLabel.LabelName;
                label.UserId = userId;
                label.NoteId = noteId;
                context.Update(label);
                await context.SaveChangesAsync();
                return label;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Update label
        public async Task<LabelEntity> UpdateLabel(long userId, long labelId, string labelName)
        {
            try
            {
                var label = await context.Labels.FirstOrDefaultAsync(x => x.UserId == userId && x.LabelId == labelId);
                if (label != null)
                {
                    label.LabelName = labelName;
                    await context.SaveChangesAsync();
                    return label;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete Label
        public async Task<bool> DeleteLabel(long userId, long labelId)
        {
            try
            {
                var label = await context.Labels.FirstOrDefaultAsync(x => x.UserId == userId && x.LabelId == labelId);
                if (label != null)
                {
                    context.Labels.Remove(label);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get All Label For User
        public async Task<IEnumerable<LabelEntity>> GetLabels(long userId)
        {
            try
            {
                return await context.Labels.Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get All Label For A node
        public async Task<IEnumerable<LabelEntity>> GetLabelsByNote(long userId, long noteId)
        {
            try
            {
                return await context.Labels.Where(x => x.UserId == userId && x.NoteId == noteId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
