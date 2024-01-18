using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;
using Stock.Domain.Entities.StorageAggregate;
using Stock.Domain.Entities.StorageItemAggregate;

namespace Stock.Domain.Entities;

public sealed class StorageCell
    : GuidKeyEntity
{
    private readonly List<StorageItem> _storageItems = new();

    public Storage.ID StorageId { get; }
    public DimensionsInfo Dimensions { get; }
    public Location Location { get; }
    public decimal RemainingVolume { get; private set; }
    public bool IsAvailable { get; }
    public string Description { get; }
    public IReadOnlyCollection<StorageItem> StockItems => _storageItems.AsReadOnly();


    private StorageCell(DimensionsInfo dimensions, Location location, bool isAvailable, string description)
      : base(new ID(Guid.NewGuid()))
    {
        Dimensions = dimensions;
        Location = location;
        RemainingVolume = dimensions.Volume;
        IsAvailable = isAvailable;
        Description = description;
    }

    public Result TryPlaceStorageItem(StorageItem storageItem)
    {
        var storageItemVolume = storageItem.Product.Dimensions.Volume;

        if (RemainingVolume < storageItemVolume)
        {
            return new Error("Cell.TryPlaceStorageItem", "Remaining volume of the cell is less than volume of product.");
        }

        _storageItems.Add(storageItem);
        RemainingVolume -= storageItemVolume;

        return Result.Success();
    }

    public Result RemoveStorageItem(StorageItem storageItem)
    {
        if(!_storageItems.Contains(storageItem))
        {
            return new Error("Cell.RemoveStorageItem", "Cell does not contain storage item.");
        }
        
        var storageItemVolume = storageItem.Product.Dimensions.Volume;

        _storageItems.Remove(storageItem);
        RemainingVolume += storageItemVolume;

        return Result.Success();
    }
}
