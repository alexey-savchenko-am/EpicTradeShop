using FluentValidation;

namespace Product.Application.Product.Queries.GetById;

public sealed class GetProductByIdQueryValidator
    : AbstractValidator<GetProductByIdQuery>
{
	public GetProductByIdQueryValidator()
	{
		RuleFor(query => query.productId)
			.NotEmpty();
	}
}
