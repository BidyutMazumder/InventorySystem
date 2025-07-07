namespace InventorySystem.Application.Features.Sales.Query.GetSalesSummary;

public class GetSalesSummaryValidator : AbstractValidator<GetSalesSummaryQuery>
{
    public GetSalesSummaryValidator()
    {
        RuleFor(x => x.request.StartDate)
            .NotEmpty().WithMessage("StartDate is required");

        RuleFor(x => x.request.EndDate)
            .NotEmpty().WithMessage("EndDate is required");

        RuleFor(x => x.request.EndDate)
            .GreaterThanOrEqualTo(x => x.request.StartDate)
            .WithMessage("EndDate must be greater than or equal to StartDate");
    }
}

