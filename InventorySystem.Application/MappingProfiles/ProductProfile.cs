

using InventorySystem.Application.Features.Products.Query.ProductList;

namespace InventorySystem.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequestDto, Product>();
        CreateMap<UpdateProductRequestDto, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        CreateMap<Product, ProductListResponseDto>();
    }
}
