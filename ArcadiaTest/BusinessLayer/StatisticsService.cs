using System.Linq;
using ArcadiaTest.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public class StatisticsService : IStatisticsService
    {
        private ITasksRepository _taskRepository;
        private ITaskStatusRepository _taskStatusRepository;

        public StatisticsService(
            ITasksRepository taskRepository,
            ITaskStatusRepository taskStatusRepository
        )
        {
            this._taskRepository = taskRepository;
            this._taskStatusRepository = taskStatusRepository;
        }

        public async Task<IDictionary<string, int>> GetStatisticsOfTaskCountGroupedByStatusAsync(int userId)
        {
            var statuses = await this._taskStatusRepository.GetAllTaskStatusesAsync();
            var groupedCounts = await this._taskRepository.CountTasksGroupedByStatusAsync(userId);

            return statuses.ToDictionary(
                s => s,
                s => groupedCounts.Where(gc => gc.Status == s).FirstOrDefault()?.Count ?? 0
            );
        }
    }
}