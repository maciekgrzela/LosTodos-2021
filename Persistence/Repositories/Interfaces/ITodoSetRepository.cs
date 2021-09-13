using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITodoSetRepository
    {
         Task<List<TodoSet>> GetAllAsync();
         Task<TodoSet> GetAsync(Guid id);
         Task<List<TodoSet>> GetAllForUserAsync(string id);
         Task<TodoSet> SearchAsync(Guid id);
         Task<TodoSet> SearchByNameAsync(string name);
         Task SaveAsync(TodoSet todoSet);
         void Update(TodoSet todoSet);
         void Delete(TodoSet todoSet);
    }
}