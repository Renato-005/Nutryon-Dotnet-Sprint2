using AutoMapper;
using Nutryon.Application.Users;
using Nutryon.Domain.Entities;

namespace Nutryon.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UsuarioCreateDto, Usuario>();
        CreateMap<UsuarioUpdateDto, Usuario>();
    }
}