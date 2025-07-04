namespace InventorySystem.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequestDto, Product>();
    }
}
