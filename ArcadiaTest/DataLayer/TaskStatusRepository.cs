using System;
using System.Collections.Generic;
using System.Linq;
using ArcadiaTest.Models.Entities;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace ArcadiaTest.DataLayer
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private ArcadiaTestEntities _dbCtx;

        public TaskStatusRepository(ArcadiaTestEntities dbContext)
        {
            this._dbCtx = dbContext;
        }

        public async Task<IEnumerable<string>> GetAllTaskStatusesAsync()
        {
            var taskStatuses = await this._dbCtx.TaskStatus.ToListAsync();
            return taskStatuses.Select(ts => ts.Name);
        }
    }
}