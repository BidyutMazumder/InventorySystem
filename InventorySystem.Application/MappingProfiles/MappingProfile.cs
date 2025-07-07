using InventorySystem.Domain.Products;

namespace InventorySystem.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddProductRequestDto, Product>();
        CreateMap<UpdateProductRequestDto, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        CreateMap<Product, ProductListResponseDto>();
        CreateMap<ProductListRequestDto, ProductSearch>();

        CreateMap<AddCustomerRequestDto, Customer>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore());
        CreateMap<UpdateCustomerRequestDto, Customer>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore());
        CreateMap<Customer, CustomerListResponseDto>();
    }
}
