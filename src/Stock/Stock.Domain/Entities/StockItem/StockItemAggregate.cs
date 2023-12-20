using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;
using Stock.Domain.Entities.Stock;

namespace Stock.Domain.Entities.StockItem;

public class StockItemAggregate
    : AggregateRoot
{
    public StockAggregate.ID StockId { get; }
    public Guid ProductId { get; set; }
    public string? Sku { get; private set; }
    public DimensionsInfo Size { get; }
    public Cell.ID? CellId { get; private set; }
    public Cell? Cell { get; private set; }

    private StockItemAggregate(StockAggregate.ID stockId, Guid productId, DimensionsInfo size)
        : base(new ID())
    { 
        StockId = stockId;
        ProductId = productId;
        Size = size;
    }

    public static Result<StockItemAggregate> Create(StockAggregate.ID stockId, Guid productId, DimensionsInfo size)
    {
        return new StockItemAggregate(stockId, productId, size);
    }

    public Result TryAssignStorageCell(Cell cell)
    {
        if(cell.RemainingVolume < Size.Volume)
        {
            return new Error("StockItemAggregate.TryAssignStorageCell", "Remaining volume of the cell is less than volume of stock item.");
        }

        var placeItemResult = cell.TryPlaceStockItem(this);

        if (placeItemResult.IsFailure)
        {
            return placeItemResult;
        }

        CellId = cell.Id;
        Cell = cell;

        return Result.Success();
    }
}
