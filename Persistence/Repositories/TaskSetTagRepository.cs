using System.Threading.Tasks;
using Domain.Models;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class TaskSetTagRepository : ITaskSetTagRepository
    {
        private readonly DatabaseContext context;

        public TaskSetTagRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async System.Threading.Tasks.Task SaveAsync(TaskSetTags taskSetTags)
        {
            await context.TaskSetTags.AddAsync(taskSetTags);
        }
    }
}