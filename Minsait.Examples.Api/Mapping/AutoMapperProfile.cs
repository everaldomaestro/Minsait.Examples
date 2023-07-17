using AutoMapper;
using Minsait.Examples.Api.ViewModels;
using Minsait.Examples.Domain.Entities;

namespace Minsait.Examples.Api.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MinsaitTest, MinsaitTestViewModel>().ReverseMap();
        }
    }
}
