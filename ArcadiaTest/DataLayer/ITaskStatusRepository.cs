using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.DataLayer
{
    public interface ITaskStatusRepository
    {
        Task<IEnumerable<string>> GetAllTaskStatusesAsync();
    }
}
