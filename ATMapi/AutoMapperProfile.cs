using AutoMapper;
using Model.DTO;
using Model.Entities;

namespace ATMapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<User, AddUserDTO>().ReverseMap();

        }
    }
}
