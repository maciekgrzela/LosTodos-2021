using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITagRepository
    {
         Task<List<Tag>> GetAllAsync();
         Task<List<Tag>> GetAllForUserAsync(string id);
         Task<Tag> GetAsync(Guid id);
         Task<Tag> SearchAsync(Guid id);
         Task<Tag> SearchByNameAsync(string name);
         System.Threading.Tasks.Task SaveAsync(Tag tag);
         void Update(Tag tag);
         void Delete(Tag tag);
    }
}