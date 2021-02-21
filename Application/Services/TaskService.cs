using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly ITaskSetRepository taskSetRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IUserAccessor userAccessor;
        public TaskService(ITaskRepository taskRepository, ITaskSetRepository taskSetRepository, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.taskSetRepository = taskSetRepository;
            this.taskRepository = taskRepository;
        }

        public async Task<Response<Domain.Models.Task>> DeleteAsync(Guid id)
        {
            var task = await taskRepository.SearchAsync(id);

            if (task == null)
            {
                return new Response<Domain.Models.Task>($"Zadanie o id:{id} nie zostało znalezione");
            }

            taskRepository.Delete(task);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.Task>(task);
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

        public async Task<Response<Domain.Models.ProductivityStat>> GetProductivityStats(int days)
        {
            if (days <= 0)
            {
                return new Response<Domain.Models.ProductivityStat>("Liczba dni musi być liczbą dodatnią!");
            }

            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return new Response<Domain.Models.ProductivityStat>("Zaloguj się do systemu w celu wykonania operacji");
            }

            var result = await taskRepository.GetProductivityStats(days, user.Id);
            return new Response<Domain.Models.ProductivityStat>(result);
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

        public async Task<Response<Domain.Models.Task>> SaveListAsync(List<Domain.Models.Task> tasks)
        {
            foreach (var task in tasks)
            {
                var saved = await this.SaveAsync(task);
                if (!saved.Success)
                {
                    return new Response<Domain.Models.Task>(saved.Message);
                }
            }
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.Task>(new Domain.Models.Task());
        }

        public async Task<Response<Domain.Models.Task>> UpdateAsync(Domain.Models.Task task, Guid id)
        {
            var currentTask = await taskRepository.SearchAsync(id);
            var taskSet = await taskSetRepository.SearchAsync(task.TaskSetId);

            if (currentTask == null)
            {
                return new Response<Domain.Models.Task>($"Zadanie o id:{id} nie zostało znalezione");
            }

            if (taskSet == null)
            {
                return new Response<Domain.Models.Task>($"Zbiór zadań o id:{id} nie został znaleziony");
            }

            currentTask.Name = task.Name;
            currentTask.TaskSet = taskSet;
            currentTask.LastChecked = currentTask.Checked != task.Checked ? DateTime.Now : null;
            currentTask.Checked = task.Checked;

            taskRepository.Update(currentTask);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Domain.Models.Task>(currentTask);
        }
    }
}