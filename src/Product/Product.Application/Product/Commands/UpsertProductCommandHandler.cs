using AppCommon.Cqrs;
using AppCommon.Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Application.Product.Commands;

internal sealed class UpsertProductCommandHandler
    : ICommandHandler<UpsertProductCommand, ProductAggregate.ID>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpsertProductCommandHandler(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductAggregate.ID>> Handle(UpsertProductCommand request, CancellationToken cancellationToken)
    {
        bool isCreate = request.ProductId is null;

        var dimensionInfoResult =
            DimensionsInfo.Create(request.Length, request.Width, request.Height, request.Weight);
        
        if (dimensionInfoResult.IsFailure)
        {
            return new Error("UpsertProductCommandHandler.Handle", dimensionInfoResult.Error.Message);
        }

        var upsertProductResult = isCreate
                ? await CreateProductAsync(request, dimensionInfoResult.Value, cancellationToken)
                : await UpdateProductAsync(request, dimensionInfoResult.Value, cancellationToken);

        if(upsertProductResult.IsFailure)
        {
            return new Error("UpsertProductCommandHandler.Handle", "Can not upsert product.", upsertProductResult.Error);
        };

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return upsertProductResult.Value;
    }

    private async Task<Result<ProductAggregate.ID>> CreateProductAsync(UpsertProductCommand request, DimensionsInfo dimensionsInfo, CancellationToken cancellationToken)
    {
        if (await _productRepository.ExistsAsync(request.Name))
        {
            return new Error("CreateProductAsync.Handle", "Product already exists");
        }

        var productResult = ProductAggregate.Create(
            request.Name,
            dimensionsInfo,
            request.Description);

        if (productResult.IsFailure)
        {
            return new Error("CreateProductAsync.Handle", "Can not create product.", productResult.Error);
        }

        SetProductFields(productResult.Value, request);

        await _productRepository.AddAsync(productResult.Value);

        return productResult.Value.Id;
    }

    private async Task<Result<ProductAggregate.ID>> UpdateProductAsync(UpsertProductCommand request, DimensionsInfo dimensionsInfo, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId.Value);

        if (product is null)
        {
            return new Error("UpdateProductAsync.Handle", "Product does not exist.");
        }

        var editProductResult = product.Edit(request.Name, request.Description, dimensionsInfo);

        if (editProductResult.IsFailure)
        {
            return new Error("UpdateProductAsync.Handle", "Can not change product.", editProductResult.Error);
        }

        SetProductFields(product, request);

        return product.Id;
    }


    private void SetProductFields(ProductAggregate product, UpsertProductCommand request)
    {

        if (request.Categories.Any())
        {
            product.AddCategories(request.Categories);
        }

        if (request.Price is not null)
        {
            product.SetProductPrice(request.Price.Value);
        }
    }

}
