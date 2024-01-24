using AppCommon.Cqrs;
using AppCommon.EventBus;
using AppCommon.Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;
using Product.Domain.Enums;
using Serilog;
using SharedKernel.Output;
using System.Net;

namespace Product.Application.Product.Commands.Approve;

internal class ApproveProductCommandHandler
    : ICommandHandler<ApproveProductCommand>
{
    private readonly IProductRepository<BaseProduct> _productRepository;
    private readonly IEventBus _eventBus;
    private readonly ISession _session;
    private readonly ILogger _logger;

    public ApproveProductCommandHandler(
        IProductRepository<BaseProduct> productRepository,
        IEventBus eventBus,
        ISession session)
    {
        _productRepository = productRepository;
        _eventBus = eventBus;
        _session = session;
        _logger = Log.ForContext<ApproveProductCommandHandler>();
    }

    public async Task<Result> Handle(ApproveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductWithFieldsAsync(request.productId, cancellationToken);
   
        if(product is null)
        {
            _logger.Error("Produst is not found by id {productId}.", request.productId.Key);

            return new HttpCodeError(
                "ApproveProductCommandHandler.Handle", 
                $"Can not find product by id {request.productId}.", 
                HttpStatusCode.NotFound);
        }

        if(product.Price is null && request.price is null)
        {
            _logger.Error("Can not approve the product {productId}, because price is not specified.", request.productId.Key);

            return new HttpCodeError(
                "ApproveProductCommandHandler.Handle",
                $"To approve the product, you must specify a price.",
                HttpStatusCode.Conflict);
        }

        var changeStatusResult = product.ChangeStatus(ProductStatus.Active);

        if(changeStatusResult.IsFailure)
        {
            _logger.Error("Can not approve the product {productId}.", request.productId.Key);

            return new HttpCodeError(
                "ApproveProductCommandHandler.Handle",
                $"Can not approve the product.",
                HttpStatusCode.Conflict,
                changeStatusResult.Error);
        }

        if(request.price is not null)
        {
            var setPriceResult = product.SetPrice(request.price);
            if(setPriceResult.IsFailure) 
            {
                return new HttpCodeError(
                    "ApproveProductCommandHandler.Handle",
                    $"Can not approve the product.",
                    HttpStatusCode.Conflict,
                    setPriceResult.Error);
            }
        }

        await _eventBus.PublishAsync(new ProductApprovedEvent(request.productId));

        await _session.StoreAsync(cancellationToken);

        return Result.Success();
    }
}
