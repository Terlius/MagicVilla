using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;

namespace MagicVilla_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Source -> Target
            CreateMap<VillaDTO, Villa>();
            CreateMap<Villa, VillaDTO>();
            
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
           
            CreateMap<NumeroVilla, NumeroVillaDTO>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaCreateDTO>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUpdateDTO>().ReverseMap();
        }


    }
}
