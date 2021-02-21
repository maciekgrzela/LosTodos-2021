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
        private readonly ITaskSetRepository taskSetRepository;
        private readonly ITaskSetTagRepository taskSetTagRepository;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IUserAccessor userAccessor, ITaskSetRepository taskSetRepository, ITaskSetTagRepository taskSetTagRepository)
        {
            this.taskSetTagRepository = taskSetTagRepository;
            this.taskSetRepository = taskSetRepository;
            this.userAccessor = userAccessor;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.tagRepository = tagRepository;
        }

        public async Task<Response<Tag>> AddTagsToTaskSetAsync(AddTagsToTaskSet addTagsToTaskSet)
        {
            var currentTaskSet = await taskSetRepository.SearchAsync(addTagsToTaskSet.TaskSetId);

            if (currentTaskSet == null)
            {
                return new Response<Tag>($"Lista zadań o id:{addTagsToTaskSet.TaskSetId} nie istnieje");
            }

            foreach (var tag in addTagsToTaskSet.Tags)
            {
                var currentTag = await tagRepository.SearchByNameAsync(tag);
                if (currentTag == null)
                {
                    var tempTag = new Tag
                    {
                        Name = tag
                    };
                    var response = await this.SaveAsync(tempTag);
                    if(!response.Success)
                    {
                        return new Response<Tag>(response.Message);
                    }

                    currentTag = response.Value;
                }

                var entity = new TaskSetTags
                {
                    TaskSet = currentTaskSet,
                    Tag = currentTag
                };

                await taskSetTagRepository.SaveAsync(entity);
            }

            await unitOfWork.CommitTransactionAsync();

            return new Response<Tag>(new Tag());
        }

        public async Task<Response<Tag>> DeleteAsync(Guid id)
        {
            var tag = await tagRepository.SearchAsync(id);

            if (tag == null)
            {
                return new Response<Tag>($"Tag o id:{id} nie został znaleziony");
            }

            tagRepository.Delete(tag);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Tag>(tag);
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await tagRepository.GetAllAsync();
        }

        public async Task<Response<List<Tag>>> GetAllForUserAsync()
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return new Response<List<Tag>>("Użytkownik nie jest aktualnie zalogowany");
            }

            var tags = await tagRepository.GetAllForUserAsync(user.Id);
            return new Response<List<Tag>>(tags);
        }

        public async Task<Response<Tag>> GetAsync(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag == null)
            {
                return new Response<Tag>($"Tag o id:{id} nie został znaleziony");
            }

            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());
            var userRole = await userManager.GetRolesAsync(user);

            if (!userRole[0].Equals("Admin"))
            {
                if (tag.OwnerId != user.Id)
                {
                    return new Response<Tag>("Tag o id:{id} nie został znaleziony");
                }
            }

            return new Response<Tag>(tag);
        }

        public async Task<Response<Tag>> SaveAsync(Tag tag)
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            var entity = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = tag.Name,
                Owner = user,
                TaskSetTags = new Collection<TaskSetTags>()
            };

            await tagRepository.SaveAsync(entity);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Tag>(entity);
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
                return new Response<Tag>($"Tag o id:{id} nie został znaleziony");
            }

            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());
            var userRole = await userManager.GetRolesAsync(user);

            if (!userRole[0].Equals("Admin"))
            {
                if (currentTag.OwnerId != user.Id)
                {
                    return new Response<Tag>("Tag o id:{id} nie został znaleziony");
                }
            }

            currentTag.Name = tag.Name;

            tagRepository.Update(currentTag);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Tag>(currentTag);
        }
    }
}