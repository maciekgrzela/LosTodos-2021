using Application.Resources.Task;
using Application.Resources.TaskSet;
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
        }
    }
}