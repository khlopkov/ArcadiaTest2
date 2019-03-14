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
        private TaskDTO entityToTaskDto(Task entity)
        {
            return entity == null ? null :
                new TaskDTO()
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    Status = entity.Status,
                    Description = entity.Description,
                    Title = entity.Name,
                    Type = entity.Type,
                    DueDate = entity.DueDate
                };
        }
        
        private Task taskDtoToEntity(TaskDTO dto)
        {
            return new Task()
            {
                Description = dto.Description,
                DueDate = dto.DueDate,
                Id = dto.Id,
                Name = dto.Title,
                Status = dto.Status,
                Type = dto.Type,
                UserId = dto.UserId
            };
        }
        
        private Task findTaskById(int id)
        {
            return this._dbCtx.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        private IEnumerable<TaskDTO> entityEnumerableToTaskDTOEnumerable(IReadOnlyCollection<Task> entities)
        {
            var taskDtos = new List<TaskDTO>(entities.Count);
            foreach (var entity in entities)
            {
                taskDtos.Add(this.entityToTaskDto(entity));
            }
            return taskDtos;
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
            return this.entityToTaskDto(foundTask);
        }

        public IEnumerable<TaskDTO> FindTasksByUserId(int userId)
        {
            return entityEnumerableToTaskDTOEnumerable(
                this._dbCtx.Tasks.Where(t => t.UserId == userId).ToList()
            );
        }

        public IEnumerable<TaskDTO> FindTasksByUserIdAndStatus(int userId, string status)
        {
            return entityEnumerableToTaskDTOEnumerable(
                this._dbCtx.Tasks.Where(t => t.UserId == userId && t.Status == status).ToList()
            );
        }

        public TaskDTO Save(TaskDTO task)
        {
            var taskEntity = taskDtoToEntity(task);
            var inserted = this._dbCtx.Tasks.Add(taskEntity);
            this._dbCtx.SaveChanges();
            return entityToTaskDto(inserted);
        }

        public TaskDTO Update(TaskDTO task)
        {
            var taskEntity = this.findTaskById(task?.Id ?? 0);
            if (taskEntity == null)
            {
                return null;
            }
            taskEntity.Description = task.Description;
            taskEntity.Name = task.Title;
            taskEntity.DueDate = task.DueDate;
            taskEntity.Type = task.Type;
            taskEntity.Status = task.Status;
            this._dbCtx.SaveChanges();
            return this.entityToTaskDto(taskEntity);
        }

        public void Delete(TaskDTO task)
        {
            var taskEntity =this.findTaskById(task?.Id ?? 0);
            this._dbCtx.Tasks.Remove(taskEntity);
            this._dbCtx.SaveChanges();
        }
    }
}