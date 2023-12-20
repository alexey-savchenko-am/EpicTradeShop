using AppCommon.Cqrs;
using SharedKernel;
using Stock.Domain.Entities.StockItem;

namespace Stock.Application.Stock.Commands;

public sealed class PlaceProductsOnStockCommand
    : ICommand<PlaceProductsOnStockResponse>

{
    public List<ProductPlacement> ProductsToPlace { get; set; } = new();
    public sealed record ProductPlacement(AggregateRoot.ID ProductId, int Quantity);
}


