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
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IUserAccessor userAccessor;
        private readonly ITodoSetRepository todoSetRepository;
        private readonly ITodoSetTagRepository todoSetTagRepository;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IUserAccessor userAccessor, ITodoSetRepository todoSetRepository, ITodoSetTagRepository todoSetTagRepository)
        {
            this.todoSetTagRepository = todoSetTagRepository;
            this.todoSetRepository = todoSetRepository;
            this.userAccessor = userAccessor;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.tagRepository = tagRepository;
        }

        public async Task<Response<Tag>> AddTagsToTodoSetAsync(AddTagsToTodoSet addTagsToTodoSet)
        {
            var currentTodoSet = await todoSetRepository.SearchAsync(addTagsToTodoSet.TodoSetId);

            if (currentTodoSet == null)
            {
                return Response<Tag>.Failure(ResponseResult.ResourceDoesntExist, $"Lista zadań o id:{addTagsToTodoSet.TodoSetId} nie została znaleziona");
            }

            foreach (var tag in addTagsToTodoSet.Tags)
            {
                var currentTag = await tagRepository.SearchByNameAsync(tag);
                if (currentTag == null)
                {
                    var tempTag = new Tag { Name = tag };
                    
                    var response = await SaveAsync(tempTag);
                    if(!response.Succeed)
                        return Response<Tag>.Failure(response.Result, response.ErrorMessage);
                    

                    currentTag = response.Value;
                }

                var entity = new TodoSetTags
                {
                    TodoSet = currentTodoSet,
                    Tag = currentTag
                };

                await todoSetTagRepository.SaveAsync(entity);
            }

            await unitOfWork.CommitTransactionAsync();

            return Response<Tag>.Success(ResponseResult.Updated);
        }

        public async Task<Response<Tag>> DeleteAsync(Guid id)
        {
            var tag = await tagRepository.SearchAsync(id);

            if (tag == null)
            {
                return Response<Tag>.Failure(ResponseResult.ResourceDoesntExist, $"Tag o id:{id} nie został znaleziony");
            }

            tagRepository.Delete(tag);
            await unitOfWork.CommitTransactionAsync();

            return Response<Tag>.Success(ResponseResult.Deleted);
        }

        public async Task<Response<List<Tag>>> GetAllAsync()
        {
            var tags = await tagRepository.GetAllAsync();
            return Response<List<Tag>>.Success(ResponseResult.DataObtained, tags);
        }

        public async Task<Response<List<Tag>>> GetAllForUserAsync()
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return Response<List<Tag>>.Failure(ResponseResult.UserIsNotAuthorized, "Użytkownik nie jest aktualnie zalogowany");
            }

            var tags = await tagRepository.GetAllForUserAsync(user.Id);
            return Response<List<Tag>>.Success(ResponseResult.DataObtained, tags);
        }

        public async Task<Response<Tag>> GetAsync(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag == null)
            {
                return Response<Tag>.Failure(ResponseResult.ResourceDoesntExist,$"Tag o id:{id} nie został znaleziony");
            }

            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());
            var userRole = await userManager.GetRolesAsync(user);

            if (!userRole[0].Equals("Admin") && tag.OwnerId != user.Id)
            {
                return Response<Tag>.Failure(ResponseResult.ResourceDoesntExist, "Tag o id:{id} nie został znaleziony");
            }

            return Response<Tag>.Success(ResponseResult.DataObtained, tag);
        }

        public async Task<Response<Tag>> SaveAsync(Tag tag)
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return Response<Tag>.Failure(ResponseResult.UserIsNotAuthorized,
                    "Użytkownik nie jest aktualnie zalogowany");
            }

            var entity = new Tag
            {
                Id = Guid.NewGuid(),
                Name = tag.Name,
                Owner = user,
                TodoSetTags = new Collection<TodoSetTags>()
            };

            await tagRepository.SaveAsync(entity);
            await unitOfWork.CommitTransactionAsync();

            return Response<Tag>.Success(ResponseResult.Created);
        }

        public async Task<Tag> SearchAsync(Guid id)
        {
            return await tagRepository.SearchAsync(id);
        }

        public async Task<Tag> SearchByNameAsync(string name)
        {
            return await tagRepository.SearchByNameAsync(name);
        }

        public async Task<Response<Tag>> UpdateAsync(Tag tag, Guid id)
        {
            var currentTag = await tagRepository.SearchAsync(id);

            if (currentTag == null)
            {
                return Response<Tag>.Failure(ResponseResult.ResourceDoesntExist, $"Tag o id:{id} nie został znaleziony");
            }

            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());
            var userRole = await userManager.GetRolesAsync(user);

            if (!userRole[0].Equals("Admin") && currentTag.OwnerId != user.Id)
            {
                return Response<Tag>.Failure(ResponseResult.ResourceDoesntExist, $"Tag o id:{id} nie został znaleziony");
            }

            currentTag.Name = tag.Name;

            tagRepository.Update(currentTag);
            await unitOfWork.CommitTransactionAsync();

            return Response<Tag>.Success(ResponseResult.Updated);
        }
    }
}