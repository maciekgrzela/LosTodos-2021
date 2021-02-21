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
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext context;
        public TagRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Delete(Tag tag)
        {
            context.Tags.Remove(tag);
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await context.Tags
                            .Include(p => p.TaskSetTags)
                            .ThenInclude(p => p.TaskSet)
                            .Include(p => p.Owner)
                            .ToListAsync();
        }

        public async Task<List<Tag>> GetAllForUserAsync(string id)
        {
            return await context.Tags
                            .Include(p => p.TaskSetTags)
                            .ThenInclude(p => p.TaskSet)
                            .Include(p => p.Owner)
                            .Where(p => p.OwnerId == id)
                            .ToListAsync();
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            return await context.Tags
                            .Include(p => p.TaskSetTags)
                            .ThenInclude(p => p.TaskSet)
                            .Include(p => p.Owner)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async System.Threading.Tasks.Task SaveAsync(Tag tag)
        {
            await context.Tags.AddAsync(tag);
        }

        public async Task<Tag> SearchAsync(Guid id)
        {
            return await context.Tags.FindAsync(id);
        }

        public async Task<Tag> SearchByNameAsync(string name)
        {
            return await context.Tags.FirstOrDefaultAsync(p => p.Name == name);
        }

        public void Update(Tag tag)
        {
            context.Tags.Update(tag);
        }
    }
}