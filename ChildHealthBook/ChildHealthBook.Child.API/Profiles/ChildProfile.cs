using AutoMapper;
using ChildHealthBook.Child.API.Models;
using ChildHealthBook.Common.WebDtos.ChildDtos;

namespace ProductsAPI.Profiles
{
    public class ChildProfile : Profile
    {
        public ChildProfile()
        {
            CreateMap<ChildModel, ChildReadDto>();
            CreateMap<ChildCreateDto, ChildModel> ();
        }
    }
}
