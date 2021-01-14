using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;

namespace Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<Domain.Models.Task>> GetAllAsync();
        Task<Response<Domain.Models.Task>> GetAsync(Guid id);
        Task<Response<Domain.Models.Task>> SaveAsync(Domain.Models.Task task);
    }
}