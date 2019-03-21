using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.DTO;
using Entities = ArcadiaTest.Models.Entities;
using ArcadiaTest.DataLayer.Exceptions;
using System.Data.Entity;

namespace ArcadiaTest.DataLayer
{
    public class TaskRepository : ITasksRepository
    {
        Entities.ArcadiaTestEntities _dbCtx;

        public TaskRepository(Entities.ArcadiaTestEntities dbCtx)
        {
            this._dbCtx = dbCtx;
        }

        private async Task<Entities.Task> FindTaskEntityByIdAsync(int id)
        {
            return await this._dbCtx.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TaskDTO> FindTaskByIdAsync(int id)
        {
            var entity =  await this._dbCtx.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
            return entity.ToDto();
        }

        public async Task<TaskDTO> SaveAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            var taskEntity = new Entities.Task();
            taskEntity.MergeWithDto(task);

            var inserted = this._dbCtx.Tasks.Add(taskEntity);
            await this._dbCtx.SaveChangesAsync();

            return inserted.ToDto();
        }

        public async Task<TaskDTO> UpdateAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.Id == 0)
                throw new IdWasNotSpecifiedException();
            
            var taskEntity = await this.FindTaskEntityByIdAsync(task.Id);
            if (taskEntity == null)
                return null;

            taskEntity.MergeWithDto(task);
            await this._dbCtx.SaveChangesAsync();

            return taskEntity.ToDto();
        }

        public async Task<IEnumerable<TaskDTO>> FindTasksByUserIdAsync(int userId)
        {
            var tasks = await this._dbCtx.Tasks.Where(t => t.UserId == userId).ToListAsync();
            return tasks.ToDtos();
        }

        public async Task<IEnumerable<TaskDTO>> FindTasksByUserIdAndStatusAsync(int userId, string status)
        {
            var tasks = await this._dbCtx.Tasks.Where(t => t.UserId == userId && t.Status == status).ToListAsync();
            return tasks.ToDtos();
        }

        public async Task<IEnumerable<TasksDashboardDTO>> CountTasksGroupedByStatusAsync(int userId)
        {
            return await this._dbCtx.Tasks.Where(t => t.UserId == userId)
                .GroupBy(t => t.Status)
                .Select(group => new TasksDashboardDTO() { Status = group.Key, Count = group.Count() }).ToListAsync();
        }

        public async Task DeleteAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.Id == 0)
                throw new IdWasNotSpecifiedException();

            var taskEntity = await this.FindTaskEntityByIdAsync(task.Id);
            this._dbCtx.Tasks.Remove(taskEntity);
            await this._dbCtx.SaveChangesAsync();
        }
    }

    public static class TaskExtensions
    {
        public static TaskDTO ToDto(this Entities.Task taskEntity)
        {
            return taskEntity == null ? null :
                new TaskDTO()
                {
                    Id = taskEntity.Id,
                    UserId = taskEntity.UserId,
                    Status = taskEntity.Status,
                    Description = taskEntity.Description,
                    Title = taskEntity.Name,
                    Type = taskEntity.Type,
                    DueDate = taskEntity.DueDate
                };
        }

        public static void MergeWithDto(this Entities.Task taskEntity, TaskDTO dto)
        {
            taskEntity.Id = dto.Id;
            taskEntity.UserId = dto.UserId == 0 ? taskEntity.UserId : dto.UserId;
            taskEntity.Status = dto.Status;
            taskEntity.Description = dto.Description;
            taskEntity.Name = dto.Title;
            taskEntity.Type = dto.Type;
            taskEntity.DueDate = dto.DueDate;
        }

        public static IEnumerable<TaskDTO> ToDtos(this IEnumerable<Entities.Task> taskEntities)
        {
            return taskEntities.Select(te => te.ToDto()).ToList();
        }
    }
}