using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
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

        public void Delete(Domain.Models.Task task)
        {
            context.Tasks.Remove(task);
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

        public async Task<Domain.Models.ProductivityStat> GetProductivityStats(int days, string id)
        {
            var stats = new ProductivityStat()
            {
                TaskCreated = 0,
                TaskChecked = 0
            };

            var createdTasks = await context.Tasks
                                    .Include(p => p.TaskSet)
                                    .Where(p => DateTime.Now.AddDays(Convert.ToDouble(-days)).CompareTo(p.Created) <= 0 && p.TaskSet.OwnerId == id)
                                    .ToListAsync();
                                    
            if(createdTasks != null)
            {
                stats.TaskCreated = createdTasks.Count;
            }

            var checkedTasks = createdTasks.Where(p => p.Checked == true).ToList();

            if(checkedTasks != null)
            {
                stats.TaskChecked = checkedTasks.Count;
            }

            return stats;
        }

        public async System.Threading.Tasks.Task SaveAsync(Domain.Models.Task task)
        {
            await context.Tasks.AddAsync(task);
        }

        public async Task<Domain.Models.Task> SearchAsync(Guid id)
        {
            return await context.Tasks.FindAsync(id);
        }

        public void Update(Domain.Models.Task task)
        {
            context.Tasks.Update(task);
        }
    }
}