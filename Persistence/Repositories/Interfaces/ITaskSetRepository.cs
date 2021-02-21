using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITaskSetRepository
    {
         System.Threading.Tasks.Task<List<TaskSet>> GetAllAsync();
         System.Threading.Tasks.Task<TaskSet> GetAsync(Guid id);
         System.Threading.Tasks.Task<List<TaskSet>> GetAllForUserAsync(string id);
         System.Threading.Tasks.Task<TaskSet> SearchAsync(Guid id);
         System.Threading.Tasks.Task<TaskSet> SearchByNameAsync(string name);
         System.Threading.Tasks.Task SaveAsync(TaskSet taskSet);
         void Update(TaskSet taskSet);
         void Delete(TaskSet taskSet);
    }
}