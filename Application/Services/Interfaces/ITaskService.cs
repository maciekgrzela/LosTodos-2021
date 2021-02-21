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
        Task<Response<Domain.Models.ProductivityStat>> GetProductivityStats(int days);
        Task<Response<Domain.Models.Task>> SaveAsync(Domain.Models.Task task);
        Task<Response<Domain.Models.Task>> SaveListAsync(List<Domain.Models.Task> tasks);
        Task<Response<Domain.Models.Task>> UpdateAsync(Domain.Models.Task task, Guid id);
        Task<Response<Domain.Models.Task>> DeleteAsync(Guid id);
    }
}