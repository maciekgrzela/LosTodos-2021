using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITaskSetService
    {
        Task<List<TaskSet>> GetAllAsync();
        Task<Response<TaskSet>> GetAsync(Guid id);
        Task<Response<TaskSet>> SaveAsync(TaskSet taskSet);
        Task<Response<TaskSet>> UpdateAsync(TaskSet taskSet, Guid id);
        Task<Response<TaskSet>> DeleteAsync(Guid id);
    }
}