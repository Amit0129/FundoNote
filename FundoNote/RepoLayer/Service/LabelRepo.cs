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
    /// <summary>
    /// Label repo layer
    /// </summary>
    public class LabelRepo : ILabelRepo
    {
        private readonly FundoContext context;
        public LabelRepo(FundoContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Add Lebel
        /// </summary>
        /// <param name="addLabel">Label info</param>
        /// <param name="userId">User Id</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>Label Info</returns>
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Update a Label
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="labelId">Label Id</param>
        /// <param name="labelName">Label Info</param>
        /// <returns>Updated label info</returns>
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete Label
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="labelId">Label Id</param>
        /// <returns>boolean Value</returns>
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Gell All Label info
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Get all info of Label</returns>
        public async Task<IEnumerable<LabelEntity>> GetLabels(long userId)
        {
            try
            {
                return await context.Labels.Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get all label for a note
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="noteId">Note Id</param>
        /// <returns>labels info for a note</returns>
        public async Task<IEnumerable<LabelEntity>> GetLabelsByNote(long userId, long noteId)
        {
            try
            {
                return await context.Labels.Where(x => x.UserId == userId && x.NoteId == noteId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
