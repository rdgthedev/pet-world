// using AutoMapper;
// using PetWorldOficial.Application.DTOs.Product;
// using PetWorldOficial.Application.DTOs.Product.Output;
// using PetWorldOficial.Application.Services.Interfaces;
// using PetWorldOficial.Domain.Entities;
// using PetWorldOficial.Domain.Exceptions;
// using PetWorldOficial.Domain.Interfaces.Repositories;
//
// namespace PetWorldOficial.Application.Services.Implementations;
//
// public class ProductService : IProductService
// {
//     private readonly IProductRepository _productRepository;
//     private readonly IMapper _mapper;
//     
//     public ProductService(
//         IProductRepository productRepository,
//         IMapper mapper)
//     {
//         _productRepository = productRepository;
//         _mapper = mapper;
//     }
//     
//     public async Task<IEnumerable<OutputProductDTO>> GetAll(CancellationToken cancellationToken)
//     {
//         var products = _mapper.Map<IEnumerable<OutputProductDTO>>(await _productRepository.GetAllAsync(cancellationToken));
//         if (products == null) throw new NotFoundException("Nenhum produto encontrado!");
//         return products;
//     }
//
//     public async Task<OutputProductDTO> GetById(int id, CancellationToken cancellationToken)
//     {
//         var product = _mapper.Map<OutputProductDTO>(await _productRepository.GetByIdAsync(id, cancellationToken));
//         if (product == null) throw new NotFoundException("Produto não encontrado");
//         return product;
//     }
//
//     public async Task Create(RegisterProductDTO productRegisterDto, CancellationToken cancellationToken)
//     {
//         var product = _mapper.Map<Product>(productRegisterDto);
//         await _productRepository.CreateAsync(product, cancellationToken);
//     }
//
//     public async Task Update(UpdateProductDTO updateProductDto, CancellationToken cancellationToken)
//     {
//         var product = _mapper.Map<Product>(updateProductDto);
//         await _productRepository.UpdateAsync(product, cancellationToken);
//     }
//
//     public async Task Delete(DeleteProductDTO deleteProductDto, CancellationToken cancellationToken)
//     {
//         var product = _mapper.Map<Product>(deleteProductDto);
//         await _productRepository.DeleteAsync(product, cancellationToken);
//     }
// }