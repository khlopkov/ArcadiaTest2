using ArcadiaTest.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public class StatisticsService : IStatisticsService
    {
        private ITasksRepository _taskRepository;
        private ITaskStatusRepository _taskStatusRepository;

        public StatisticsService (
            ITasksRepository taskRepository,
            ITaskStatusRepository taskStatusRepository
        )
        {
            this._taskRepository = taskRepository;
            this._taskStatusRepository = taskStatusRepository;
        }

        public async Task<IDictionary<string, int>> GetStatisticsOfTaskCountGroupedByStatus(int userId)
        {
            var result = new Dictionary<string, int>();

            var statuses = await this._taskStatusRepository.FindAllTaskStatusesAsync();
            var groupedCounts = await this._taskRepository.CountTasksGroupedByStatusAsync(userId);

            foreach (var taskCount in groupedCounts)
            {
                result.Add(taskCount.Status, taskCount.Count);
            }

            foreach (var status in statuses)
            {
                if (!result.ContainsKey(status))
                    result.Add(status, 0);
            }

            return result;
        }
    }
}