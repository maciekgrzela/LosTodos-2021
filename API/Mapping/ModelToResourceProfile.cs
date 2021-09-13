using System.Collections.Generic;
using Application.Resources.Tag;
using Application.Resources.Todo;
using Application.Resources.TodoSet;
using Application.Resources.User;
using Application.Responses;
using AutoMapper;
using Domain.Models;

namespace API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Tag, TagResource>()
                .ForMember(dest => dest.TodoSets, opt => opt.MapFrom(p => p.TodoSetTags));
            CreateMap<Response<List<Tag>>, Response<List<TagResource>>>();
            CreateMap<Response<Tag>, Response<TagResource>>();
            CreateMap<Tag, MyTagResource>()
                .ForMember(dest => dest.TodoSets, opt => opt.MapFrom(p => p.TodoSetTags));
            CreateMap<Response<List<Tag>>, Response<List<MyTagResource>>>();
            CreateMap<Response<Tag>, Response<MyTagResource>>();
            CreateMap<Todo, TodoResource>()
                .ForMember(dest => dest.TodoSet, opt => opt.MapFrom(p => p.TodoSet));
            CreateMap<Response<List<Todo>>, Response<List<TodoResource>>>();
            CreateMap<Response<Todo>, Response<TodoResource>>();
            CreateMap<Todo, TodoForTodoSetResource>();
            CreateMap<TodoSet, TodoSetResource>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.TodoSetTags));
            CreateMap<Response<List<TodoSet>>, Response<List<TodoSetResource>>>();
            CreateMap<Response<TodoSet>, Response<TodoSetResource>>();
            CreateMap<TodoSet, MyTodoSetResource>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.TodoSetTags));
            CreateMap<Response<List<TodoSet>>, Response<List<MyTodoSetResource>>>();
            CreateMap<Response<TodoSet>, Response<MyTodoSetResource>>();
            CreateMap<TodoSetTags, TodoSetForTagResource>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.TodoSet.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.TodoSet.Name))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(p => p.TodoSet.LastModified))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(p => p.TodoSet.Created));
            CreateMap<Response<List<TodoSetTags>>, Response<List<TodoSetForTagResource>>>();
            CreateMap<Response<TodoSetTags>, Response<TodoSetForTagResource>>();
            CreateMap<TodoSet, SetForTodoResource>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.TodoSetTags));
            CreateMap<Response<List<TodoSet>>, Response<List<SetForTodoResource>>>();
            CreateMap<Response<TodoSet>, Response<SetForTodoResource>>();
            CreateMap<TodoSetTags, TagForTodoSetResource>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.Tag.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Tag.Name))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(p => p.Tag.LastModified))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(p => p.Tag.Created));
            CreateMap<Response<List<TodoSetTags>>, Response<List<TagForTodoSetResource>>>();
            CreateMap<Response<TodoSetTags>, Response<TagForTodoSetResource>>();
            CreateMap<LoggedUser, LoggedUserResource>();
            CreateMap<Response<List<LoggedUser>>, Response<List<LoggedUserResource>>>();
            CreateMap<Response<LoggedUser>, Response<LoggedUserResource>>();
            CreateMap<AppUser, OwnerResource>();
            CreateMap<Response<List<AppUser>>, Response<List<OwnerResource>>>();
            CreateMap<Response<AppUser>, Response<OwnerResource>>();
            CreateMap<ProductivityStat, ProductivityStatResource>();
            CreateMap<Response<List<ProductivityStat>>, Response<List<ProductivityStatResource>>>();
            CreateMap<Response<ProductivityStat>, Response<ProductivityStatResource>>();
        }
        
    }
}