using SharedKernel;
using Stock.Domain.Entities.StockItem;

namespace Stock.Application.Stock.Commands;


public sealed class PlaceProductsOnStockResponse
{
    public List<PlacedProduct> PlacedProducts { get; set; } = new();
    public sealed record PlacedProduct(AggregateRoot.ID ProductId, StockItemAggregate.ID StockItemId);
}
