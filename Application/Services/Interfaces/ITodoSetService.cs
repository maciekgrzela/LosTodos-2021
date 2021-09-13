using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITodoSetService
    {
        Task<Response<List<TodoSet>>> GetAllAsync();
        Task<Response<List<TodoSet>>> GetAllForUserAsync();
        Task<Response<TodoSet>> GetAsync(Guid id);
        Task<Response<TodoSet>> SaveAsync(TodoSet todoSet);
        Task<Response<TodoSet>> UpdateAsync(TodoSet todoSet, Guid id);
        Task<Response<TodoSet>> DeleteAsync(Guid id);
    }
}