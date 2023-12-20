using AppCommon.Cqrs;
using SharedKernel.Output;

namespace Stock.Application.Stock.Commands;

public sealed class PlaceProductsOnStockCommandHandler
    : ICommandHandler<PlaceProductsOnStockCommand, PlaceProductsOnStockResponse>
{
    public Task<Result<PlaceProductsOnStockResponse>> Handle(PlaceProductsOnStockCommand request, CancellationToken cancellationToken)
    {
        return null;
        // 1. request information about sizes of products from Product service 

        // 2. create StockItems based on Products info

        // 3. find free cells on Stock and try to arrange created StockItems there 
    }
}
