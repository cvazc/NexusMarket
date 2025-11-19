using AutoMapper;
using Nexus.Catalog.API.DTOs;
using Nexus.Catalog.API.Entities;

namespace Nexus.Catalog.API.Prodiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();
        }
    }
}