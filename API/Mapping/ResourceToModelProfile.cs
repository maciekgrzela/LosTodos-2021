using Application.Resources.Tag;
using Application.Resources.Todo;
using Application.Resources.TodoSet;
using Application.Resources.User;
using AutoMapper;
using Domain.Models;

namespace API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveTodoResource, Todo>();
            CreateMap<SaveTodoSetResource, TodoSet>();
            CreateMap<SaveTagResource, Tag>();
            CreateMap<UserCredentialsResource, UserCredentials>();
            CreateMap<AddTagsToTodoSetResource, AddTagsToTodoSet>();
            CreateMap<UpdateUserResource, UpdateUser>();
            CreateMap<RegisterCredentialsResource, RegisterCredentials>();
        }
    }
}