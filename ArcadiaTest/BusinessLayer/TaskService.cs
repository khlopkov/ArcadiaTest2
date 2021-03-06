﻿using ArcadiaTest.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.Models.Entities;
using ArcadiaTest.BusinessLayer.Exceptions;

namespace ArcadiaTest.BusinessLayer
{
    public class TaskService : ITaskService
    {
        public const string ACTIVE = "Active";
        public const string RESOLVED = "Resolved";
        public const string CANCELLED = "Cancelled";

        private ITasksRepository _taskRepository;
        private IUserRepository _userRepository;

        public TaskService
        (
            ITasksRepository taskRepository,
            IUserRepository userRepository
        )
        {
            this._taskRepository = taskRepository;
            this._userRepository = userRepository;
        }

        public void CreateTask(int userId, CreateTaskRequest payload)
        {
            if (this._userRepository.FindUserByID(userId) == null)
                throw new UserNotFoundException(userId);

            var taskEntity = new Task
            {
                UserId = userId,
                Name = payload.Title,
                Description = payload.Description,
                Status = ACTIVE,
                Type = payload.Type,
                DueDate = payload.DueDate,
            };
            this._taskRepository.Save(taskEntity);
        }

        public void DeleteTask(int taskId)
        {
            var task = this._taskRepository.FindTaskById(taskId);
            if (task == null)
                throw new TaskNotFoundException(taskId);
            this._taskRepository.Delete(task);
        }

        public TaskResponse GetTask(int id)
        {
            var taskEntity = this._taskRepository.FindTaskById(id);
            if (taskEntity == null)
                throw new TaskNotFoundException(id);
            return new TaskResponse
            {
                Id = taskEntity.Id,
                Title = taskEntity.Name,
                Description = taskEntity.Description,
                Status = taskEntity.Status,
                DueDate = taskEntity.DueDate,
                Type = taskEntity.Type,
            };
        }

        public DashboardResponse GetTasksDashboard(int userId)
        {
            DashboardResponse response = new DashboardResponse();
            var groupedCounts = this._taskRepository.CountTasksGroupedByStatus(userId);
            foreach (var taskCount in groupedCounts)
            {
                switch (taskCount.Status)
                {
                    case ACTIVE:
                        response.Active = taskCount.Count;
                        break;
                    case RESOLVED:
                        response.Resolved = taskCount.Count;
                        break;
                    case CANCELLED:
                        response.Cancelled = taskCount.Count;
                        break;
                    default:
                        continue;
                }
            }
            return response;
        }

        public IEnumerable<TaskResponse> GetTasksOfUser(int userId)
        {
            var taskEntities = this._taskRepository.FindTasksByUserId(userId);
            var response = new List<TaskResponse>();
            foreach (var taskEntity in taskEntities)
            {
                response.Add(new TaskResponse
                {
                    Id = taskEntity.Id,
                    Title = taskEntity.Name,
                    Description = taskEntity.Description,
                    DueDate = taskEntity.DueDate,
                    Status = taskEntity.Status,
                    Type = taskEntity.Type,
                });
            }
            return response;
        }

        public IEnumerable<TaskResponse> GetTasksOfUser(int userId, string status)
        {
            var taskEntities = this._taskRepository.FindTasksByUserIdAndStatus(userId, status);
            var response = new List<TaskResponse>();
            foreach (var taskEntity in taskEntities)
            {
                response.Add(new TaskResponse
                {
                    Id = taskEntity.Id,
                    Title = taskEntity.Name,
                    Description = taskEntity.Description,
                    DueDate = taskEntity.DueDate,
                    Status = taskEntity.Status,
                    Type = taskEntity.Type,
                });
            }
            return response;
        }

        public void UpdateTask(int id, MergeTaskRequest updateModel)
        {
            var taskEntity = this._taskRepository.FindTaskById(id);
            if (taskEntity == null)
                throw new TaskNotFoundException(id);
            if (taskEntity.Status != ACTIVE)
                throw new TaskNotActiveException();
            taskEntity.Name = !String.IsNullOrEmpty(updateModel.Title) ?  updateModel.Title : taskEntity.Name;
            if (updateModel.Description != null)
            {
                if (updateModel.Description == "")
                    taskEntity.Description = null;
                else
                    taskEntity.Description = updateModel.Description;
            }
            if (updateModel.DueDate != null)
            {
                if (updateModel.DueDate == default(DateTime))
                    taskEntity.DueDate = null;
                else
                    taskEntity.DueDate = updateModel.DueDate;
            }
            if (updateModel.Status != null)
            {
                if (updateModel.Status == "")
                    taskEntity.Status = null;
                else
                    taskEntity.Status = updateModel.Status;
            }
            if (updateModel.Type != null)
            {
                if (updateModel.Type == "")
                    taskEntity.Type = null;
                else
                    taskEntity.Type = updateModel.Type;
            }
            this._taskRepository.Update(taskEntity);
        }
    }
}