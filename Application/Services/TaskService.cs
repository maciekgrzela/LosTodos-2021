using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly ITaskSetRepository taskSetRepository;
        private readonly IUnitOfWork unitOfWork;
        public TaskService(ITaskRepository taskRepository, ITaskSetRepository taskSetRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.taskSetRepository = taskSetRepository;
            this.taskRepository = taskRepository;
        }
        public async Task<List<Domain.Models.Task>> GetAllAsync()
        {
            return await taskRepository.GetAllAsync();
        }

        public async Task<Response<Domain.Models.Task>> GetAsync(Guid id)
        {
            var task = await taskRepository.GetAsync(id);

            if (task == null)
            {
                return new Response<Domain.Models.Task>($"Zadanie o id:{id} nie zostało znalezione");
            }

            return new Response<Domain.Models.Task>(task);
        }

        public async Task<Response<Domain.Models.Task>> SaveAsync(Domain.Models.Task task)
        {
            var taskSet = await taskSetRepository.SearchAsync(task.TaskSetId);

            if (taskSet == null)
            {
                return new Response<Domain.Models.Task>($"Zbiór o id:{task.TaskSetId} do którego ma zostać przypisane zadanie nie istnieje");
            }

            var entity = new Domain.Models.Task()
            {
                Id = Guid.NewGuid(),
                Name = task.Name,
                Checked = task.Checked,
                LastChecked = task.Checked ? DateTime.Now : null,
                TaskSet = taskSet
            };


            await taskRepository.SaveAsync(entity);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.Task>(entity);
        }
    }
}