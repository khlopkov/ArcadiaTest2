using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.DataLayer
{
    public class TaskChangesRepository : ITaskChangesRepository
    {
        private ArcadiaTestEntities _dbCtx;

        public TaskChangesRepository(ArcadiaTestEntities dbCtx)
        {
            this._dbCtx = dbCtx;
        }

        public IEnumerable<TaskChange> FindChangesByTaskID(int taskId)
        {
            return this._dbCtx.TaskChanges.Where(tc => tc.TaskId == taskId).ToList();
        }

        public IEnumerable<TaskChangeDTO> FindChangesByUserId(int userId)
        {
            return this._dbCtx.TaskChanges.Select(tc => new TaskChangeDTO()
            {
                Id = tc.Id,
                TaskId = tc.TaskId,
                ChangedAt = tc.ChangedAt,
                NewValue = tc.NewValue,
                OldValue = tc.OldValue,
                Operation = tc.Operation,
                Task = this._dbCtx.Tasks.Where(t => t.Id == tc.TaskId).FirstOrDefault(),
            })
                .Where(tc => tc.Task.UserId == userId).ToList();
        }
    }
}