using AppCommon.Cqrs;
using AppCommon.Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;

namespace Product.Application.Product.Commands;

internal class ApproveProductCommandHandler
    : ICommandHandler<ApproveProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(ApproveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId);
        if (product is null)
        {
            return Result.Failure(new Error("ApproveProductCommandHandler.Handle", "Product does not exist."));
        }

        if(product.Price is null && request.ProductPrice is null)
        {
            return Result.Failure(new Error("ApproveProductCommandHandler.Handle", "You should specify price of the product while activation."));
        }

        var changeStatusResult = product.ChangeStatus(ProductStatus.Active);
        if(changeStatusResult.IsFailure)
        {
            return Result.Failure(new Error("ApproveProductCommandHandler.Handle", "Can not activate product.", changeStatusResult.Error));
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
