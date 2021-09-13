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
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository todoRepository;
        private readonly ITodoSetRepository todoSetRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IUserAccessor userAccessor;
        
        public TodoService(ITodoRepository todoRepository, ITodoSetRepository todoSetRepository, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.todoSetRepository = todoSetRepository;
            this.todoRepository = todoRepository;
        }

        public async Task<Response<Todo>> DeleteAsync(Guid id)
        {
            var todo = await todoRepository.SearchAsync(id);

            if (todo == null)
            {
                return Response<Todo>.Failure(ResponseResult.ResourceDoesntExist, $"Zadanie o id:{id} nie zostało znalezione");
            }

            todoRepository.Delete(todo);
            await unitOfWork.CommitTransactionAsync();

            return Response<Todo>.Success(ResponseResult.Deleted);
        }

        public async Task<Response<List<Todo>>> GetAllAsync()
        {
            var todos = await todoRepository.GetAllAsync();
            return Response<List<Todo>>.Success(ResponseResult.DataObtained, todos);
        }

        public async Task<Response<Todo>> GetAsync(Guid id)
        {
            var todo = await todoRepository.GetAsync(id);

            if (todo == null)
            {
                return Response<Todo>.Failure(ResponseResult.ResourceDoesntExist, $"Zadanie o id:{id} nie zostało znalezione");
            }

            return Response<Todo>.Success(ResponseResult.DataObtained, todo);
        }

        public async Task<Response<ProductivityStat>> GetProductivityStats(int days)
        {
            if (days <= 0)
            {
                return Response<ProductivityStat>.Failure(ResponseResult.BadRequestStructure, "Liczba dni musi być liczbą dodatnią!");
            }

            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return Response<ProductivityStat>.Failure(ResponseResult.UserIsNotAuthorized,"Zaloguj się do systemu w celu wykonania operacji");
            }

            var result = await todoRepository.GetProductivityStats(days, user.Id);
            return Response<ProductivityStat>.Success(ResponseResult.DataObtained, result);
        }

        public async Task<Response<Todo>> SaveAsync(Todo todo)
        {
            var todoSet = await todoSetRepository.SearchAsync(todo.TodoSetId);

            if (todoSet == null)
            {
                return Response<Todo>.Failure(ResponseResult.ResourceDoesntExist, $"Zbiór o id:{todo.TodoSetId} do którego ma zostać przypisane zadanie nie istnieje");
            }

            var entity = new Todo
            {
                Id = Guid.NewGuid(),
                Name = todo.Name,
                Checked = todo.Checked,
                LastChecked = todo.Checked ? DateTime.Now : null,
                TodoSet = todoSet
            };


            await todoRepository.SaveAsync(entity);
            await unitOfWork.CommitTransactionAsync();

            return Response<Todo>.Success(ResponseResult.Created);
        }

        public async Task<Response<Todo>> SaveListAsync(List<Todo> todos)
        {
            foreach (var todo in todos)
            {
                var saved = await SaveAsync(todo);
                if (!saved.Succeed)
                {
                    return Response<Todo>.Failure(saved.Result, saved.ErrorMessage);
                }
            }
            await unitOfWork.CommitTransactionAsync();

            return Response<Todo>.Success(ResponseResult.Created);
        }

        public async Task<Response<Todo>> UpdateAsync(Todo todo, Guid id)
        {
            var currentTodo = await todoRepository.SearchAsync(id);
            var todoSet = await todoSetRepository.SearchAsync(todo.TodoSetId);

            if (currentTodo == null)
            {
                return Response<Todo>.Failure(ResponseResult.ResourceDoesntExist, $"Zadanie o id:{id} nie zostało znalezione");
            }

            if (todoSet == null)
            {
                return Response<Todo>.Failure(ResponseResult.ResourceDoesntExist, $"Zbiór zadań o id:{id} nie został znaleziony");
            }

            currentTodo.Name = todo.Name;
            currentTodo.TodoSet = todoSet;
            currentTodo.LastChecked = currentTodo.Checked != todo.Checked ? DateTime.Now : null;
            currentTodo.Checked = todo.Checked;

            todoRepository.Update(currentTodo);
            await unitOfWork.CommitTransactionAsync();

            return Response<Todo>.Success(ResponseResult.Updated);
        }
    }
}