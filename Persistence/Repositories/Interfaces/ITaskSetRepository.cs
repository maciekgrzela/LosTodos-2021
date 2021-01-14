using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITaskSetRepository
    {
         Task<List<TaskSet>> GetAllAsync();
         Task<TaskSet> GetAsync(Guid id);
         Task<TaskSet> SearchAsync(Guid id);
    }
}