using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public interface IStatisticsService
    {
        Task<IDictionary<string, int>> GetStatisticsOfTaskCountGroupedByStatusAsync(int userId);
    }
}
