using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
        }
    }
}
