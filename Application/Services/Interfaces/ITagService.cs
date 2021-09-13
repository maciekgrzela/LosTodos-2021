using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITagService
    {
        Task<Response<List<Tag>>> GetAllAsync();
        Task<Response<List<Tag>>> GetAllForUserAsync();
        Task<Response<Tag>> GetAsync(Guid id);
        Task<Tag> SearchAsync(Guid id);
        Task<Tag> SearchByNameAsync(string name);
        Task<Response<Tag>> SaveAsync(Tag tag);
        Task<Response<Tag>> AddTagsToTodoSetAsync(AddTagsToTodoSet addTagsToTodoSet);
        Task<Response<Tag>> UpdateAsync(Tag tag, Guid id);
        Task<Response<Tag>> DeleteAsync(Guid id);
    }
}