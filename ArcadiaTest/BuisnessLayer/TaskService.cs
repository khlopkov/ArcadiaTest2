using ArcadiaTest.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.Models.Entities;
using ArcadiaTest.BuisnessLayer.Exceptions;

namespace ArcadiaTest.BuisnessLayer
{
    public class TaskService : ITaskService
    {
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
                Status = "Active",
                Type = payload.Type,
                DueDate = payload.DueDate,
            };
            this._taskRepository.Save(taskEntity);
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
                    case "Active":
                        response.Active = taskCount.Count;
                        break;
                    case "Resolved":
                        response.Resolved = taskCount.Count;
                        break;
                    case "Cancelled":
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

        public void PatchTask(int id, MergeTaskRequest patchModel)
        {
            var taskEntity = this._taskRepository.FindTaskById(id);
            if (taskEntity == null)
                throw new TaskNotFoundException(id);
            if (taskEntity.Status != "Active")
                throw new TaskNotActiveException();
            taskEntity.Name = !String.IsNullOrEmpty(patchModel.Title) ?  patchModel.Title : taskEntity.Name;
            if (patchModel.Description != null)
            {
                if (patchModel.Description == "")
                    taskEntity.Description = null;
                else
                    taskEntity.Description = patchModel.Description;
            }
            if (patchModel.DueDate != null)
            {
                if (patchModel.DueDate != default(DateTime))
                    taskEntity.DueDate = null;
                else
                    taskEntity.DueDate = patchModel.DueDate;
            }
            if (patchModel.Status != null)
            {
                if (patchModel.Status == "")
                    taskEntity.Status = null;
                else
                    taskEntity.Status = patchModel.Status;
            }
            if (patchModel.Type != null)
            {
                if (patchModel.Type == "")
                    taskEntity.Type = null;
                else
                    taskEntity.Type = patchModel.Type;
            }
            this._taskRepository.Update(taskEntity);
        }
    }
}