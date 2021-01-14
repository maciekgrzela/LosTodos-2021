using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class TaskSetRepository : ITaskSetRepository
    {
        private readonly DatabaseContext context;

        public TaskSetRepository(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task<List<TaskSet>> GetAllAsync()
        {
            return await context.TaskSets
                                .Include(p => p.Tasks)
                                .Include(p => p.TaskSetTags)
                                .ThenInclude(p => p.Tag)
                                .ToListAsync();
        }

        public async Task<TaskSet> GetAsync(Guid id)
        {
            return await context.TaskSets
                                .Include(p => p.Tasks)
                                .Include(p => p.TaskSetTags)
                                .ThenInclude(p => p.Tag)
                                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<TaskSet> SearchAsync(Guid id)
        {
            return await context.TaskSets.FindAsync(id);
        }
    }
}