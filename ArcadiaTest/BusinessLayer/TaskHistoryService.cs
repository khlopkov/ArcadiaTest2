using ArcadiaTest.DataLayer;
using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArcadiaTest.BusinessLayer
{
    public class TaskHistoryService : ITaskHistoryService
    {
        private TaskChangesRepository _taskChangesRepository;

        public TaskHistoryService(TaskChangesRepository taskChangesRepository)
        {
            this._taskChangesRepository = taskChangesRepository;
        }

        public IEnumerable<TaskChangeResponse> GetTasksHistoryOfUser(int userId)
        {
            return this._taskChangesRepository.FindChangesByUserId(userId)
                .Select(c => this.DtoToResponse(c));
        }

        public async Task<IEnumerable<TaskChangeResponse>> GetTasksHistoryOfUserAsync(int userId)
        {
            var changes = await this._taskChangesRepository.FindChangesByUserIdAsync(userId);
            return changes.Select(c => this.DtoToResponse(c));
        }

        private TaskChangeResponse DtoToResponse(TaskChangeDTO dto)
        {
            var message = dto.Operation;
            message += dto.OldValue == null ? "" : " from: " + dto.OldValue;
            message += dto.NewValue == null ? "" : " to: " + dto.NewValue;

            return new TaskChangeResponse()
            {
                Message = message,
                When = dto.ChangedAt,
            };
        }
    }
}