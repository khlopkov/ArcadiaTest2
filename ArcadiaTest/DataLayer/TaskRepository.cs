﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public int CountActiveTasksByUserId(int userId)
        {
            return this._dbCtx.Tasks.Count(t => t.UserId == userId && t.Status == "Active");
        }

        public int CountClosedTasksByUserId(int userId)
        {
            return this._dbCtx.Tasks.Count(t => t.UserId == userId && t.Status == "Closed");
        }

        public int CountResolvedTasksByUserId(int userId)
        {
            return this._dbCtx.Tasks.Count(t => t.UserId == userId && t.Status == "Resolved");
        }

        public Task FindTaskById(int id)
        {
            return this._dbCtx.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<Task> FindTasksByUserId(int userId)
        {
            return this._dbCtx.Tasks.Where(t => t.UserId == userId).ToList();
        }

        public Task Save(Task task)
        {
            var inserted = this._dbCtx.Tasks.Add(task);
            this._dbCtx.SaveChanges();
            return inserted;
        }

        public Task UpdateDescription(int taskId, string description)
        {
            var found = this._dbCtx.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (found == null)
            {
                return null;
            }
            found.Description = description;
            this._dbCtx.SaveChanges();
            return found;
        }

        public Task UpdateDueDate(int taskId, DateTime? dueDate)
        {
            var found = this._dbCtx.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (found == null)
            {
                return null;
            }
            found.DueDate = dueDate;
            this._dbCtx.SaveChanges();
            return found;
        }

        public Task UpdateName(int taskId, string name)
        {
            var found = this._dbCtx.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (found == null)
            {
                return null;
            }
            found.Name = name;
            this._dbCtx.SaveChanges();
            return found;
        }

        public Task UpdateStatus(int taskId, string status)
        {
            var found = this._dbCtx.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (found == null)
            {
                return null;
            }
            found.Status = status;
            this._dbCtx.SaveChanges();
            return found;
        }

        public Task UpdateType(int taskId, string type)
        {
            var found = this._dbCtx.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (found == null)
            {
                return null;
            }
            found.Type = type;
            this._dbCtx.SaveChanges();
            return found;
        }
    }
}