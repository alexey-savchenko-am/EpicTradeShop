using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;
using Stock.Domain.Entities.Stock;
using Stock.Domain.Entities.StockItem;

namespace Stock.Domain.Entities;

public sealed class Cell
    : GuidKeyEntity
{
    private readonly List<StockItemAggregate> _stockItems = new();

    public StockAggregate.ID StockId { get; }
    public IReadOnlyCollection<StockItemAggregate> StockItems => _stockItems.AsReadOnly();
    public DimensionsInfo Size { get; }
    public Location Location { get; }
    public decimal RemainingVolume { get; private set; }
    public bool IsAvailable { get; } 
    public string Description { get; } 

    private Cell(DimensionsInfo size, Location location, bool isAvailable, string description)
      : base(new ID())
    {
        Size = size;
        Location = location;    
        RemainingVolume = size.Volume;
        IsAvailable = isAvailable;
        Description = description;
    }

    public Result TryPlaceStockItem(StockItemAggregate stockItem)
    {
        if (RemainingVolume < stockItem.Size.Volume)
        {
            return new Error("Cell.PlaceStockItem", "Remaining volume of the cell is less than volume of stock item.");
        }

        _stockItems.Add(stockItem);
        RemainingVolume -= stockItem.Size.Volume;

        return Result.Success();
    }
}
