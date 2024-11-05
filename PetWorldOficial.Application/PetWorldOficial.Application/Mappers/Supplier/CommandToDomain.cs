using AutoMapper;
using PetWorldOficial.Application.Commands.Supplier;

namespace PetWorldOficial.Application.Mappers.Supplier;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<RegisterSupplierCommand, Domain.Entities.Supplier>();
        CreateMap<UpdateSupplierCommand, Domain.Entities.Supplier>();
        CreateMap<DeleteSupplierCommand, Domain.Entities.Supplier>();
    }
}