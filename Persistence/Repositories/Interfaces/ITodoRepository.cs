using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo> GetAsync(Guid id);
        Task<ProductivityStat> GetProductivityStats(int days, string id);
        Task<Todo> SearchAsync(Guid id);
        Task SaveAsync(Todo todo);
        void Update(Todo todo);
        void Delete(Todo todo);
    }
}