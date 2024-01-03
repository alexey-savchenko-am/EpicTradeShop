using AppCommon.Cqrs;
using AppCommon.Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Enums;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Application.Product.Commands.Create;

public abstract class BaseCreateProductCommandHandler<TConcreteProductRequest, TConcreteProduct>
    : ICommandHandler<CreateProductCommand<TConcreteProductRequest>, BaseProduct.ID>
    where TConcreteProductRequest : class
    where TConcreteProduct : BaseProduct
{
    private readonly IProductRepository<TConcreteProduct> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    private static Error CreateProductError(Error innerError) =>
        new("Product.Create", "Can not create new product.", innerError);
    public BaseCreateProductCommandHandler(
        IProductRepository<TConcreteProduct> productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    protected abstract Result<TConcreteProduct> CreateConcreteProduct(TConcreteProductRequest request, ProductEntities productEntities);

    public async Task<Result<BaseProduct.ID>> Handle(CreateProductCommand<TConcreteProductRequest> request, CancellationToken cancellationToken)
    {
        var productEntities = CreateBaseEntities(request);
        if (productEntities.IsFailure)
        {
            return CreateProductError(productEntities.Error);
        }
        var concreteProduct = CreateConcreteProduct(request.Product, productEntities);

        if(request.Price is not null)
        {
            var moneyResult = Money.CreateUsd(request.Price.Value);
            if (moneyResult.IsFailure)
            {
                return CreateProductError(moneyResult.Error);
            }
            concreteProduct.Value.SetPrice(moneyResult);
        }

        if (request.Categories.Any())
        {
            concreteProduct.Value.AddCategories(request.Categories);
        }

        await _productRepository.AddAsync(concreteProduct, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return concreteProduct.Value.Id;
    }

    protected Result<ProductEntities> CreateBaseEntities<TModel>(CreateProductCommand<TModel> command)
        where TModel: class
    {
        var productDetailsResult = ProductDetails.Create(command.Name, command.Description);

        if (productDetailsResult.IsFailure)
        {
            return CreateProductError(productDetailsResult.Error);
        }

        if(!Enum.TryParse<Brand>(command.Brand, ignoreCase: true, out var brand))
        {
            return CreateProductError(new Error("", $"Unknown brand type {command.Brand}"));
        }

        var brandModelResult = BrandModel.Create(brand, command.Model);

        if (brandModelResult.IsFailure)
        {
            return CreateProductError(brandModelResult.Error);
        }

        var dimensionsResult =  DimensionsInfo.Create(command.Length, command.Width, command.Height, command.Weight);

        if (dimensionsResult.IsFailure)
        {
            return CreateProductError(dimensionsResult.Error);
        }

        return new ProductEntities(productDetailsResult, brandModelResult, dimensionsResult);
    }

    protected class ProductEntities
    {
        public ProductEntities(
            ProductDetails productDetails, 
            BrandModel brandModel, 
            DimensionsInfo dimensionsInfo)
        {
            ProductDetails = productDetails;
            BrandModel = brandModel;
            DimensionsInfo = dimensionsInfo;
        }
        public ProductDetails ProductDetails { get; }
        public BrandModel BrandModel { get; }
        public DimensionsInfo DimensionsInfo { get; }
    }
}
