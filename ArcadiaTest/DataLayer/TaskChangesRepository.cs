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

        public IEnumerable<TaskChangeDTO> FindChangesByTaskID(int taskId)
        {
            return this._dbCtx.TaskChanges.Where(tc => tc.TaskId == taskId).ToList().ToDtos();
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

    public static class TaskChangeExtension
    {
        public static TaskChangeDTO ToDto(this TaskChange entity)
        {
            return entity == null ? null :
                new TaskChangeDTO()
                {
                    Id = entity.Id,
                    ChangedAt = entity.ChangedAt,
                    NewValue = entity.NewValue,
                    OldValue = entity.OldValue,
                    Operation = entity.Operation,
                    Task = entity.Task,
                    TaskId = entity.TaskId
                };
        }

        public static IEnumerable<TaskChangeDTO> ToDtos(this IReadOnlyCollection<TaskChange> taskChangeEntities)
        {
            return taskChangeEntities.Select(tce => tce.ToDto()).ToList();
        }
    }
}