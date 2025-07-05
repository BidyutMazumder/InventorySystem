namespace InventorySystem.Application.Features.Products.Query.ProductList;

public class ProductListValidator : AbstractValidator<ProductListQuery>
{
    public ProductListValidator()
    {
        RuleFor(x => x.request.Page)
            .GreaterThan(0).WithMessage("Page must be greater than 0.");

        RuleFor(x => x.request.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
    }
}

