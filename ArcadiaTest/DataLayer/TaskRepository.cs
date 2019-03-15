﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;
using ArcadiaTest.DataLayer.Exceptions;
using System.Data.Entity;

namespace ArcadiaTest.DataLayer
{
    public class TaskRepository : ITasksRepository
    {
        ArcadiaTestEntities _dbCtx;

        public TaskRepository(ArcadiaTestEntities dbCtx)
        {
            this._dbCtx = dbCtx;
        }

        private Task FindTaskEntityById(int id)
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
            var foundTask = this.FindTaskEntityById(id);
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
            if (task == null)
                throw new ArgumentNullException("task");
            var taskEntity = new Task();
            taskEntity.MergeWithDto(task);
            var inserted = this._dbCtx.Tasks.Add(taskEntity);
            this._dbCtx.SaveChanges();
            return inserted.ToDto();
        }

        public TaskDTO Update(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (task.Id == 0)
                throw new IdWasNotSpecifiedException();
            var taskEntity = this.FindTaskEntityById(task.Id);
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
            if (task == null)
                throw new ArgumentNullException("task");
            if (task.Id == 0)
                throw new IdWasNotSpecifiedException();
            var taskEntity = this.FindTaskEntityById(task.Id);
            this._dbCtx.Tasks.Remove(taskEntity);
            this._dbCtx.SaveChanges();
        }
        public async System.Threading.Tasks.Task<TaskDTO> FindTaskByIdAsync(int id)
        {
            var entity =  await this._dbCtx.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
            return entity.ToDto();
        }

        public async System.Threading.Tasks.Task<TaskDTO> SaveAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            var taskEntity = new Models.Entities.Task();
            taskEntity.MergeWithDto(task);
            var inserted = this._dbCtx.Tasks.Add(taskEntity);
            await this._dbCtx.SaveChangesAsync();
            return inserted.ToDto();
        }

        public async System.Threading.Tasks.Task<TaskDTO> UpdateAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (task.Id == 0)
                throw new IdWasNotSpecifiedException();
            var taskEntity = this.FindTaskEntityById(task.Id);
            if (taskEntity == null)
            {
                return null;
            }
            taskEntity.MergeWithDto(task);
            await this._dbCtx.SaveChangesAsync();
            return taskEntity.ToDto();
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskDTO>> FindTasksByUserIdAsync(int userId)
        {
            Console.WriteLine("jkfdsl");
            var tasks = await this._dbCtx.Tasks.Where(t => t.UserId == userId).ToListAsync();
            Console.WriteLine("jkfdsl");
            return tasks.ToDtos();
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskDTO>> FindTasksByUserIdAndStatusAsync(int userId, string status)
        {
            var tasks = await this._dbCtx.Tasks.Where(t => t.UserId == userId && t.Status == status).ToListAsync();
            return tasks.ToDtos();
        }

        public async System.Threading.Tasks.Task<IEnumerable<TasksDashboardDTO>> CountTasksGroupedByStatusAsync(int userId)
        {
            return await this._dbCtx.Tasks.Where(t => t.UserId == userId)
                .GroupBy(t => t.Status)
                .Select(group => new TasksDashboardDTO() { Status = group.Key, Count = group.Count() }).ToListAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (task.Id == 0)
                throw new IdWasNotSpecifiedException();
            var taskEntity = this.FindTaskEntityById(task.Id);
            this._dbCtx.Tasks.Remove(taskEntity);
            await this._dbCtx.SaveChangesAsync();
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
            return taskEntities.Select(te => te.ToDto()).ToList();
        }
    }
}