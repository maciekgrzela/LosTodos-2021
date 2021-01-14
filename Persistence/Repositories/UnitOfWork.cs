using System.Threading.Tasks;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext context;
        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task CommitTransactionAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}