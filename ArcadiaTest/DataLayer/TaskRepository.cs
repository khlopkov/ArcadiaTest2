using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;

namespace ArcadiaTest.DataLayer
{
    public class TaskRepository : ITasksRepository
    {
        ArcadiaTestEntities _dbCtx;

        public TaskRepository(ArcadiaTestEntities dbCtx)
        {
            this._dbCtx = dbCtx;
        }
        
        private Task findTaskById(int id)
        {
            return this._dbCtx.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<TasksDashboardDTO> CountTasksGroupedByStatus(int userId)
        {
            return this._dbCtx.Tasks.Where(t => t.UserId == userId)
                .GroupBy(t => t.Status)
                .Select(group => new TasksDashboardDTO() { Status = group.Key, Count = group.Count() }).ToList();
        }

        public TaskDTO FindTaskById(int id)
        {
            var foundTask = this.findTaskById(id);
            return foundTask.ToDto();
        }

        public IEnumerable<TaskDTO> FindTasksByUserId(int userId)
        {
            return this._dbCtx.Tasks.Where(t => t.UserId == userId).ToList().ToDtos();
        }

        public IEnumerable<TaskDTO> FindTasksByUserIdAndStatus(int userId, string status)
        {
            return this._dbCtx.Tasks.Where(t => t.UserId == userId && t.Status == status).ToList().ToDtos();
        }

        public TaskDTO Save(TaskDTO task)
        {
            var taskEntity = new Task();
            taskEntity.MergeWithDto(task);
            var inserted = this._dbCtx.Tasks.Add(taskEntity);
            this._dbCtx.SaveChanges();
            return inserted.ToDto();
        }

        public TaskDTO Update(TaskDTO task)
        {
            var taskEntity = this.findTaskById(task?.Id ?? 0);
            if (taskEntity == null)
            {
                return null;
            }
            taskEntity.MergeWithDto(task);
            this._dbCtx.SaveChanges();
            return taskEntity.ToDto();
        }

        public void Delete(TaskDTO task)
        {
            var taskEntity =this.findTaskById(task?.Id ?? 0);
            this._dbCtx.Tasks.Remove(taskEntity);
            this._dbCtx.SaveChanges();
        }
    }

    public static class TaskExtensions
    {
        public static TaskDTO ToDto(this Task taskEntity)
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
        public static void MergeWithDto(this Task taskEntity, TaskDTO dto)
        {
            taskEntity.Id = dto.Id;
            taskEntity.UserId = dto.UserId == 0 ? taskEntity.UserId : dto.UserId;
            taskEntity.Status = dto.Status;
            taskEntity.Description = dto.Description;
            taskEntity.Name = dto.Title;
            taskEntity.Type = dto.Type;
            taskEntity.DueDate = dto.DueDate;
        }
        public static IEnumerable<TaskDTO> ToDtos(this IReadOnlyCollection<Task> taskEntities)
        {
            var taskDtos = new List<TaskDTO>(taskEntities.Count);
            foreach (var entity in taskEntities)
            {
                taskDtos.Add(entity.ToDto());
            }
            return taskDtos;
        }
    }
}