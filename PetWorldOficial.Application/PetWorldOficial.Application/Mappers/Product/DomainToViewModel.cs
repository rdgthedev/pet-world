using AutoMapper;
using PetWorldOficial.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Mappers.Product
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Domain.Entities.Product, ProductDetailsViewModel>()
                .ConstructUsing(p => new ProductDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    CategoryName = p.Category.Title,
                    SupplierName= p.Supplier.Name,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId,
                    Supplier = p.Supplier
                });
        }
    }
}
