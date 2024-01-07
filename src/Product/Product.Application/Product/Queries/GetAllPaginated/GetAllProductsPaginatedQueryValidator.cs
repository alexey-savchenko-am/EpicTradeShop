using FluentValidation;

namespace Product.Application.Product.Queries.GetAllPaginated;

public sealed class GetAllProductsPaginatedQueryValidator
    : AbstractValidator<GetAllProductsPaginatedQuery>
{
	public GetAllProductsPaginatedQueryValidator()
	{
		RuleFor(query => query.Page)
			.NotEmpty()
			.GreaterThan(0);

		RuleFor(query => query.ProductsPerPage)
            .NotEmpty()
            .GreaterThan(4);
	}
}
