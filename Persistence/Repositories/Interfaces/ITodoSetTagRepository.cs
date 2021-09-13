using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITodoSetTagRepository
    {
        Task SaveAsync(TodoSetTags todoSetTags);
    }
}