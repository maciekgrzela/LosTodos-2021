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
    public class TodoSetService : ITodoSetService
    {
        private readonly ITodoSetRepository todoSetRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagService tagService;
        private readonly ITodoService todoService;
        private readonly UserManager<AppUser> userManager;
        private readonly IUserAccessor userAccessor;

        public TodoSetService(ITodoSetRepository todoSetRepository, ITagService tagService, ITodoService todoService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.userManager = userManager;
            this.todoService = todoService;
            this.tagService = tagService;
            this.unitOfWork = unitOfWork;
            this.todoSetRepository = todoSetRepository;
        }

        public async Task<Response<TodoSet>> DeleteAsync(Guid id)
        {
            var todoSet = await todoSetRepository.SearchAsync(id);

            if (todoSet == null)
            {
                return Response<TodoSet>.Failure(ResponseResult.ResourceDoesntExist,$"Zbiór zadań o id:{id} nie został znaleziony");
            }

            todoSetRepository.Delete(todoSet);
            await unitOfWork.CommitTransactionAsync();

            return Response<TodoSet>.Success(ResponseResult.Deleted);
        }

        public async Task<Response<List<TodoSet>>> GetAllAsync()
        {
            var todoSets = await todoSetRepository.GetAllAsync();
            return Response<List<TodoSet>>.Success(ResponseResult.DataObtained, todoSets);
        }

        public async Task<Response<List<TodoSet>>> GetAllForUserAsync()
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return Response<List<TodoSet>>.Failure(ResponseResult.UserIsNotAuthorized, "Użytkownik nie jest aktualnie zalogowany");
            }

            var todoSets = await todoSetRepository.GetAllForUserAsync(user.Id);
            return Response<List<TodoSet>>.Success(ResponseResult.DataObtained, todoSets);
        }

        public async Task<Response<TodoSet>> GetAsync(Guid id)
        {
            var todoSet = await todoSetRepository.GetAsync(id);

            if (todoSet == null)
            {
                return Response<TodoSet>.Failure(ResponseResult.ResourceDoesntExist, $"Zbiór zadań o id:{id} nie został znaleziony");
            }

            return Response<TodoSet>.Success(ResponseResult.DataObtained, todoSet);
        }

        public async Task<Response<TodoSet>> SaveAsync(TodoSet todoSet)
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if(user == null)
            {
                return Response<TodoSet>.Failure(ResponseResult.UserIsNotAuthorized, "Zaloguj się do systemu w celu wykonania operacji");
            }


            var entity = new TodoSet
            {
                Id = Guid.NewGuid(),
                Name = todoSet.Name,
                Owner = user,
                Todos = new Collection<Todo>(),
                TodoSetTags = new Collection<TodoSetTags>()
            };

            await todoSetRepository.SaveAsync(entity);
            await unitOfWork.CommitTransactionAsync();

            return Response<TodoSet>.Success(ResponseResult.Created);
        }

        public async Task<Response<TodoSet>> UpdateAsync(TodoSet todoSet, Guid id)
        {
            var currentTodoSet = await todoSetRepository.SearchAsync(id);

            if (currentTodoSet == null)
            {
                return Response<TodoSet>.Failure(ResponseResult.ResourceDoesntExist,$"Zbiór zadań o id:{id} nie został znaleziony");
            }

            currentTodoSet.Name = todoSet.Name;

            todoSetRepository.Update(currentTodoSet);
            await unitOfWork.CommitTransactionAsync();

            return Response<TodoSet>.Success(ResponseResult.Updated);
        }
    }
}