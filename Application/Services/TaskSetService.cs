using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class TaskSetService : ITaskSetService
    {
        private readonly ITaskSetRepository taskSetRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagService tagService;
        private readonly ITaskService taskService;
        private readonly UserManager<AppUser> userManager;
        private readonly IUserAccessor userAccessor;

        public TaskSetService(ITaskSetRepository taskSetRepository, ITagService tagService, ITaskService taskService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.userManager = userManager;
            this.taskService = taskService;
            this.tagService = tagService;
            this.unitOfWork = unitOfWork;
            this.taskSetRepository = taskSetRepository;
        }

        public async Task<Response<TaskSet>> DeleteAsync(Guid id)
        {
            var taskSet = await taskSetRepository.SearchAsync(id);

            if (taskSet == null)
            {
                return new Response<TaskSet>($"Zbiór zadań o id:{id} nie został znaleziony");
            }

            taskSetRepository.Delete(taskSet);
            await unitOfWork.CommitTransactionAsync();

            return new Response<TaskSet>(taskSet);
        }

        public async Task<List<TaskSet>> GetAllAsync()
        {
            return await taskSetRepository.GetAllAsync();
        }

        public async Task<Response<List<TaskSet>>> GetAllForUserAsync()
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return new Response<List<TaskSet>>("Użytkownik nie jest aktualnie zalogowany");
            }

            var taskSets = await taskSetRepository.GetAllForUserAsync(user.Id);
            return new Response<List<TaskSet>>(taskSets);
        }

        public async Task<Response<TaskSet>> GetAsync(Guid id)
        {
            var taskSet = await taskSetRepository.GetAsync(id);

            if (taskSet == null)
            {
                return new Response<TaskSet>($"Zbiór zadań o id:{id} nie został znaleziony");
            }

            return new Response<TaskSet>(taskSet);
        }

        public async Task<Response<TaskSet>> SaveAsync(TaskSet taskSet)
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if(user == null)
            {
                return new Response<TaskSet>("Zaloguj się do systemu w celu wykonania operacji");
            }


            var entity = new TaskSet()
            {
                Id = Guid.NewGuid(),
                Name = taskSet.Name,
                Owner = user,
                Tasks = new Collection<Domain.Models.Task>(),
                TaskSetTags = new Collection<TaskSetTags>()
            };

            await taskSetRepository.SaveAsync(entity);
            await unitOfWork.CommitTransactionAsync();

            return new Response<TaskSet>(entity);
        }

        public async Task<Response<TaskSet>> UpdateAsync(TaskSet taskSet, Guid id)
        {
            var currentTaskSet = await taskSetRepository.SearchAsync(id);

            if (currentTaskSet == null)
            {
                return new Response<TaskSet>($"Zbiór zadań o id:{id} nie został znaleziony");
            }

            currentTaskSet.Name = taskSet.Name;

            taskSetRepository.Update(currentTaskSet);
            await unitOfWork.CommitTransactionAsync();

            return new Response<TaskSet>(currentTaskSet);
        }
    }
}