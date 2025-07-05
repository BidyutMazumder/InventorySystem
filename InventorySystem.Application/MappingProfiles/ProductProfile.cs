using InventorySystem.Application.Features.Customers.Commands.UpdateCustomer;

namespace InventorySystem.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequestDto, Product>();
        CreateMap<UpdateProductRequestDto, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        CreateMap<Product, ProductListResponseDto>();

        CreateMap<AddCustomerRequestDto, Customer>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore());
        CreateMap<UpdateCustomerRequestDto, Customer>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore());
    }
}
