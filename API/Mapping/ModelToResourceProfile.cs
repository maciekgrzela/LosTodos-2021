using Application.Resources.Tag;
using Application.Resources.Task;
using Application.Resources.TaskSet;
using Application.Resources.User;
using AutoMapper;
using Domain.Models;

namespace API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Tag, TagResource>()
                .ForMember(dest => dest.TaskSets, opt => opt.MapFrom(p => p.TaskSetTags));
            CreateMap<Tag, MyTagResource>()
                .ForMember(dest => dest.TaskSets, opt => opt.MapFrom(p => p.TaskSetTags));
            CreateMap<Task, TaskResource>()
                .ForMember(dest => dest.TaskSet, opt => opt.MapFrom(p => p.TaskSet));
            CreateMap<Task, TaskForTaskSetResource>();
            CreateMap<TaskSet, TaskSetResource>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.TaskSetTags));
            CreateMap<TaskSet, MyTaskSetResource>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.TaskSetTags));
            CreateMap<TaskSetTags, TaskSetForTagResource>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.TaskSet.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.TaskSet.Name))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(p => p.TaskSet.LastModified))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(p => p.TaskSet.Created));
            CreateMap<TaskSet, SetForTaskResource>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.TaskSetTags));
            CreateMap<TaskSetTags, TagForTaskSetResource>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.Tag.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.Tag.Name))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(p => p.Tag.LastModified))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(p => p.Tag.Created));
            CreateMap<LoggedUser, LoggedUserResource>();
            CreateMap<AppUser, OwnerResource>();
            CreateMap<ProductivityStat, ProductivityStatResource>();
        }
        
    }
}