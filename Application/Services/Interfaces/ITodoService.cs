using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<Response<List<Todo>>> GetAllAsync();
        Task<Response<Todo>> GetAsync(Guid id);
        Task<Response<ProductivityStat>> GetProductivityStats(int days);
        Task<Response<Todo>> SaveAsync(Todo todo);
        Task<Response<Todo>> SaveListAsync(List<Todo> todos);
        Task<Response<Todo>> UpdateAsync(Todo todo, Guid id);
        Task<Response<Todo>> DeleteAsync(Guid id);
    }
}