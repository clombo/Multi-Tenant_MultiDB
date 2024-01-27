using AutoMapper;
using MTA.Domain.Entities;
using MTA.Domain.Models;

namespace MTA.API.Mappings;

public class CreateProductRequestMapping : Profile
{
    public CreateProductRequestMapping()
    {
        CreateProductMapping();
    }

    private void CreateProductMapping()
    {
        CreateMap<CreateProductRequest, ProductEntity>()
            .ForMember(
                d => d.Id, 
                o => 
                    o.MapFrom(s => Guid.NewGuid())
                )
            .ForMember(
                d => d.Name,
                o =>
                    o.MapFrom(s => s.Name)
                )
            .ForMember(
                d => d.Price,
                o => 
                    o.MapFrom(s => s.Price)
                );
    }
}