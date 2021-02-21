using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Domain.Models.Task>> GetAllAsync();
        Task<Domain.Models.Task> GetAsync(Guid id);
        Task<Domain.Models.ProductivityStat> GetProductivityStats(int days, string id);
        Task<Domain.Models.Task> SearchAsync(Guid id);
        Task SaveAsync(Domain.Models.Task task);
        void Update(Domain.Models.Task task);
        void Delete(Domain.Models.Task task);
    }
}