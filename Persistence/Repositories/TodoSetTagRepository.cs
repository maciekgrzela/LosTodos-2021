using System.Threading.Tasks;
using Domain.Models;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class TodoSetTagRepository : ITodoSetTagRepository
    {
        private readonly DatabaseContext context;

        public TodoSetTagRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async System.Threading.Tasks.Task SaveAsync(TodoSetTags todoSetTags)
        {
            await context.TodoSetTags.AddAsync(todoSetTags);
        }
    }
}