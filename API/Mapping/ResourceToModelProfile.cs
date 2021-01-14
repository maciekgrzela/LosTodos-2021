using Application.Resources.Task;
using AutoMapper;

namespace API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveTaskResource, Domain.Models.Task>();
        }
    }
}