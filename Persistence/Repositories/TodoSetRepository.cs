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
    public class TodoSetRepository : ITodoSetRepository
    {
        private readonly DatabaseContext context;

        public TodoSetRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Delete(TodoSet todoSet)
        {
            context.TodoSets.Remove(todoSet);
        }

        public async Task<List<TodoSet>> GetAllAsync()
        {
            return await context.TodoSets
                                .Include(p => p.Todos)
                                .Include(p => p.TodoSetTags)
                                .ThenInclude(p => p.Tag)
                                .Include(p => p.Owner)
                                .OrderByDescending(p => p.Created)
                                .ToListAsync();
        }

        public async Task<List<TodoSet>> GetAllForUserAsync(string id)
        {
            return await context.TodoSets
                                .Include(p => p.Todos)
                                .Include(p => p.TodoSetTags)
                                .ThenInclude(p => p.Tag)
                                .Include(p => p.Owner)
                                .Where(p => p.OwnerId == id)
                                .OrderByDescending(p => p.Created)
                                .ToListAsync();
        }

        public async Task<TodoSet> GetAsync(Guid id)
        {
            return await context.TodoSets
                                .Include(p => p.Todos)
                                .Include(p => p.TodoSetTags)
                                .ThenInclude(p => p.Tag)
                                .Include(p => p.Owner)
                                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(TodoSet todoSet)
        {
            await context.TodoSets.AddAsync(todoSet);
        }

        public async Task<TodoSet> SearchAsync(Guid id)
        {
            return await context.TodoSets.FindAsync(id);
        }

        public async Task<TodoSet> SearchByNameAsync(string name)
        {
            return await context.TodoSets.FirstOrDefaultAsync(p => p.Name == name);
        }

        public void Update(TodoSet todoSet)
        {
            context.TodoSets.Update(todoSet);
        }
    }
}