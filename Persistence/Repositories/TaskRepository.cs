using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext context;
        public TaskRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Domain.Models.Task>> GetAllAsync()
        {
            return await context.Tasks
                        .Include(p => p.TaskSet)
                        .ThenInclude(p => p.TaskSetTags)
                        .ThenInclude(p => p.Tag)
                        .ToListAsync();
        }

        public async Task<Domain.Models.Task> GetAsync(Guid id)
        {
            return await context.Tasks
                        .Include(p => p.TaskSet)
                        .ThenInclude(p => p.TaskSetTags)
                        .ThenInclude(p => p.Tag)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(Domain.Models.Task task)
        {
            await context.Tasks.AddAsync(task);
        }
    }
}