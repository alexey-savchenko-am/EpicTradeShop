using FluentValidation;

namespace Product.Application.Product.Commands.AddImage;

public class AddImageCommandValidator
    : AbstractValidator<AddImageCommand>
{
	public AddImageCommandValidator()
	{
		RuleFor(command => command.ProductId).NotEmpty();
		RuleFor(command => command.ImageNameWithExtension).NotEmpty();
		RuleFor(command => command.ImageLink).NotEmpty();
		
		RuleFor(command => command.ImageData)
			.Must(BeNonEmpty)
			.NotEmpty();
	}

    private static bool BeNonEmpty(byte[] array)
    {
        return array != null && array.Length > 0;
    }
}
