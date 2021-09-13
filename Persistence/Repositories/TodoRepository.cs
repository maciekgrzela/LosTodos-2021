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
    public class TodoRepository : ITodoRepository
    {
        private readonly DatabaseContext context;
        public TodoRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Delete(Todo todo)
        {
            context.Todos.Remove(todo);
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await context.Todos
                        .Include(p => p.TodoSet)
                        .ThenInclude(p => p.TodoSetTags)
                        .ThenInclude(p => p.Tag)
                        .ToListAsync();
        }

        public async Task<Todo> GetAsync(Guid id)
        {
            return await context.Todos
                        .Include(p => p.TodoSet)
                        .ThenInclude(p => p.TodoSetTags)
                        .ThenInclude(p => p.Tag)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductivityStat> GetProductivityStats(int days, string id)
        {
            var stats = new ProductivityStat
            {
                TodoCreated = 0,
                TodoChecked = 0
            };

            var createdTodos = await context.Todos
                .Include(p => p.TodoSet)
                .Where(p => DateTime.Now.AddDays(Convert.ToDouble(-days)).CompareTo(p.Created) <= 0 && p.TodoSet.OwnerId == id)
                .ToListAsync();

            if(createdTodos.Count > 0)
            {
                
                stats.TodoCreated = createdTodos.Count;
            }

            var checkedTodos = createdTodos.Where(p => p.Checked).ToList();

            if(checkedTodos.Count > 0)
            {
                stats.TodoChecked = checkedTodos.Count;
            }

            return stats;
        }

        public async System.Threading.Tasks.Task SaveAsync(Todo todo)
        {
            await context.Todos.AddAsync(todo);
        }

        public async Task<Todo> SearchAsync(Guid id)
        {
            return await context.Todos.FindAsync(id);
        }

        public void Update(Todo todo)
        {
            context.Todos.Update(todo);
        }
    }
}