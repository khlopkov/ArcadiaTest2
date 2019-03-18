using ArcadiaTest.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.Models.Entities;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.Models.DTO;

namespace ArcadiaTest.BusinessLayer
{
    public class TaskService : ITaskService
    {
        public const string ACTIVE = "Active";
        public const string RESOLVED = "Resolved";
        public const string CANCELLED = "Cancelled";

        private ITasksRepository _taskRepository;
        private IUserRepository _userRepository;
        private ITaskChangesRepository _taskChangesRepository;

        public TaskService
        (
            ITaskChangesRepository taskChangesRepository,
            ITasksRepository taskRepository,
            IUserRepository userRepository
        )
        {
            this._taskChangesRepository = taskChangesRepository;
            this._taskRepository = taskRepository;
            this._userRepository = userRepository;
        }

        public void CreateTask(int userId, CreateTaskRequest payload)
        {
            if (this._userRepository.FindUserByID(userId) == null)
                throw new UserNotFoundException(userId);

            var task = new TaskDTO
            {
                UserId = userId,
                Title = payload.Title,
                Description = payload.Description,
                Status = ACTIVE,
                Type = payload.Type,
                DueDate = payload.DueDate,
            };
            this._taskRepository.Save(task);
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
                Title = taskEntity.Title,
                Description = taskEntity.Description,
                Status = taskEntity.Status,
                DueDate = taskEntity.DueDate,
                Type = taskEntity.Type,
            };
        }

        public TaskResponse GetTaskOfUser(int userId, int taskId)
        {
            var taskEntity = this._taskRepository.FindTaskById(taskId);
            if (taskEntity == null || taskEntity.UserId != userId)
            {
                throw new TaskNotFoundException();
            }
            return new TaskResponse()
            {
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                Description = taskEntity.Description,
                Status = taskEntity.Status,
                DueDate = taskEntity.DueDate,
                Type = taskEntity.Type,
            };
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
                    Title = taskEntity.Title,
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
                    Title = taskEntity.Title,
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
            taskEntity.Title = !String.IsNullOrEmpty(updateModel.Title) ?  updateModel.Title : taskEntity.Title;
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