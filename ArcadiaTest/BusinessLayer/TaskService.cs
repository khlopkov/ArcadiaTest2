using ArcadiaTest.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.Models.DTO;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public class TaskService : ITaskService
    {
        public const string ACTIVE = "Active";

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
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

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

        public async Task CreateTaskAsync(int userId, CreateTaskRequest payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            if (await this._userRepository.FindUserByIDAsync(userId) == null)
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
            await this._taskRepository.SaveAsync(task);
        }

        public void DeleteTask(int taskId)
        {
            var task = this._taskRepository.FindTaskById(taskId);
            if (task == null)
                throw new TaskNotFoundException(taskId);

            this._taskRepository.Delete(task);
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await this._taskRepository.FindTaskByIdAsync(taskId);
            if (task == null)
                throw new TaskNotFoundException(taskId);

            await this._taskRepository.DeleteAsync(task);
        }

        private TaskDTO FindTaskDtoById(int id)
        {
            var taskDto = this._taskRepository.FindTaskById(id);
            if (taskDto == null)
                throw new TaskNotFoundException(id);

            return taskDto;
        }

        private async Task<TaskDTO> FindTaskDtoByIdAsync(int id)
        {
            var taskDto = await this._taskRepository.FindTaskByIdAsync(id);
            if (taskDto == null)
                throw new TaskNotFoundException(id);

            return taskDto;
        }

        public TaskResponse GetTask(int id)
        {
            return this.FindTaskDtoById(id).ToResponse();
        }

        public async Task<TaskResponse> GetTaskAsync(int id)
        {
            var taskDto = await this.FindTaskDtoByIdAsync(id);
            return taskDto.ToResponse();
        }

        public TaskResponse GetTaskOfUser(int userId, int taskId)
        {
            var taskDto = this.FindTaskDtoById(taskId);
            if (taskDto == null || taskDto.UserId != userId)
                throw new TaskNotFoundException();

            return taskDto.ToResponse();
        }

        public async Task<TaskResponse> GetTaskOfUserAsync(int userId, int taskId)
        {
            var taskDto = await this.FindTaskDtoByIdAsync(taskId);
            if (taskDto == null || taskDto.UserId != userId)
                throw new TaskNotFoundException();

            return taskDto.ToResponse();
        }

        private IEnumerable<TaskResponse> GetTasksOfUser(int userId)
        {
            var taskDtos = this._taskRepository.FindTasksByUserId(userId);
            return taskDtos.Select(t => t.ToResponse());
        }

        private async Task<IEnumerable<TaskResponse>> GetTasksOfUserAsync(int userId)
        {
            var taskDtos = await this._taskRepository.FindTasksByUserIdAsync(userId);
            return taskDtos.Select(t => t.ToResponse());
        }

        public IEnumerable<TaskResponse> GetTasksOfUser(int userId, string status = null)
        {
            if (status == null)
                return this.GetTasksOfUser(userId);

            var taskDtos = this._taskRepository.FindTasksByUserIdAndStatus(userId, status);
            return taskDtos.Select(t => t.ToResponse());
        }

        public async Task<IEnumerable<TaskResponse>> GetTasksOfUserAsync(int userId, string status = null)
        {
            if (status == null)
                return this.GetTasksOfUser(userId);

            var taskDtos = await this._taskRepository.FindTasksByUserIdAndStatusAsync(userId, status);
            return taskDtos.Select(t => t.ToResponse());
        }

        public void UpdateTask(int id, MergeTaskRequest updateModel)
        {
            if (updateModel == null)
                throw new ArgumentNullException(nameof(updateModel));

            var taskDto = this._taskRepository.FindTaskById(id);
            if (taskDto == null)
                throw new TaskNotFoundException(id);

            if (taskDto.Status != ACTIVE)
                throw new TaskNotActiveException();

            taskDto.Title = !String.IsNullOrEmpty(updateModel.Title) ? updateModel.Title : taskDto.Title;

            taskDto.Description = updateModel.Description == "" ?
                 null : updateModel.Description;

            taskDto.DueDate = updateModel.DueDate == null ?
                null : updateModel.DueDate;

            taskDto.Status = updateModel.Status == "" ?
                taskDto.Status = null : updateModel.Status;

            taskDto.Type = updateModel.Type == "" ?
                null : updateModel.Type;

            this._taskRepository.Update(taskDto);
        }

        public async Task UpdateTaskAsync(int id, MergeTaskRequest updateModel)
        {
            if (updateModel == null)
                throw new ArgumentNullException(nameof(updateModel));

            var taskDto = await this._taskRepository.FindTaskByIdAsync(id);
            if (taskDto == null)
                throw new TaskNotFoundException(id);

            if (taskDto.Status != ACTIVE)
                throw new TaskNotActiveException();

            taskDto.Title = !String.IsNullOrEmpty(updateModel.Title) ? updateModel.Title : taskDto.Title;

            taskDto.Description = updateModel.Description == "" ?
                 null : updateModel.Description;

            taskDto.DueDate = updateModel.DueDate == null ?
                null : updateModel.DueDate;

            taskDto.Status = updateModel.Status == "" ?
                taskDto.Status = null : updateModel.Status;

            taskDto.Type = updateModel.Type == "" ?
                null : updateModel.Type;

            await this._taskRepository.UpdateAsync(taskDto);
        }
    }

    public static class TaskDTOExtension
    {
        public static TaskResponse ToResponse(this TaskDTO taskDto)
        {
            return new TaskResponse
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                DueDate = taskDto.DueDate,
                Type = taskDto.Type,
            };
        }
    }
}