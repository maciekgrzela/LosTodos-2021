using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Domain.Models.Task>> GetAllAsync();
        Task<Domain.Models.Task> GetAsync(Guid id);
        Task SaveAsync(Domain.Models.Task task);
    }
}