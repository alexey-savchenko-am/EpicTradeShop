using AppCommon.Cqrs;
using AppCommon.Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Application.Product.Commands;

internal sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if(await _productRepository.IsProductExistsAsync(request.Name))
        {
            return new Error("CreateProductCommandHandler.Handle", "Product already exists");
        }

        var dimensionInfoResult =
            DimensionsInfo.Create(request.Length, request.Width, request.Height, request.Weight);

        if (dimensionInfoResult.IsFailure)
        {
            return new Error("CreateProductCommandHandler.Handle", dimensionInfoResult.Error.Message);
        }

        var productResult = ProductAggregate.Create(
            request.Name,
            dimensionInfoResult.Value,
            request.Description);

        if (productResult.IsFailure)
        {
            return new Error("CreateProductCommandHandler.Handle", productResult.Error.Message);
        }
        
        if (request.Categories.Any())
        {
            productResult.Value.AddCategories(request.Categories);
        }

        await _productRepository.AddAsync(productResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return productResult.Value.Id.Key;
    }
}
