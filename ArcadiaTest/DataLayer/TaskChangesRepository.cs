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
            return this._dbCtx.TaskChanges
                .Where(tc => tc.Task.UserId == userId)
                .ToList()
                .Join(
                    this._dbCtx.Tasks.Where(t => t.UserId == userId).ToList(),
                    tc => tc.TaskId,
                    t => t.Id,
                    (tc, t) =>
                    {
                        var tcDto = tc.ToDto();
                        tcDto.Task = t.ToDto();
                        return tcDto;
                    }
                )
                .ToList();
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
                    Task = entity.Task.ToDto(),
                    TaskId = entity.TaskId
                };
        }

        public static IEnumerable<TaskChangeDTO> ToDtos(this IReadOnlyCollection<TaskChange> taskChangeEntities)
        {
            return taskChangeEntities.Select(tce => tce.ToDto()).ToList();
        }
    }
}