using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITaskSetTagRepository
    {
        System.Threading.Tasks.Task SaveAsync(TaskSetTags taskSetTags);
    }
}