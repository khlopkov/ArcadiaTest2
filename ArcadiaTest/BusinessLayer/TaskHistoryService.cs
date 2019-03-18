using ArcadiaTest.DataLayer;
using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var changes = this._taskChangesRepository.FindChangesByUserId(userId);
            var response = new List<TaskChangeResponse>(changes.Count());
            foreach(var change in changes)
            {
                var message = change.Operation;
                message += change.OldValue == null ? "" : " from: " + change.OldValue;
                message += change.NewValue == null ? "" : " to: " + change.NewValue;
                response.Add(new TaskChangeResponse()
                {
                    Message = message,
                    When = change.ChangedAt,
                });
            }
            return response;
        }
    }
}