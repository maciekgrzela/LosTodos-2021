using Application.Resources.Tag;
using Application.Resources.Task;
using Application.Resources.TaskSet;
using Application.Resources.User;
using AutoMapper;
using Domain.Models;

namespace API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveTaskResource, Domain.Models.Task>();
            CreateMap<SaveTaskSetResource, TaskSet>();
            CreateMap<SaveTagResource, Tag>();
            CreateMap<UserCredentialsResource, UserCredentials>();
            CreateMap<AddTagsToTaskSetResource, AddTagsToTaskSet>();
            CreateMap<UpdateUserResource, UpdateUser>();
        }
    }
}