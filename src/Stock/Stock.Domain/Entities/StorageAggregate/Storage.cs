using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;
using Stock.Domain.Entities.StorageItemAggregate;

namespace Stock.Domain.Entities.StorageAggregate;

public sealed class Storage
    : AggregateRoot
{
    private readonly List<StorageCell> _cells = new();

    public string Name { get; }
    public Address Address { get; }
    public IReadOnlyCollection<StorageCell> Cells => _cells.AsReadOnly();

    #pragma warning disable CS8618
    private Storage() : base(new ID(Guid.NewGuid())){}

    private Storage(string name, Address address, IReadOnlyCollection<StorageCell> cells)
        : base(new ID(Guid.NewGuid()))
    {
        Name = name;
        Address = address;
        _cells.AddRange(cells);
    }

    public Result AddStorageCells(params StorageCell[] cells)
    {
        _cells.AddRange(cells); 
        return Result.Success();
    }

    public Result<StorageCell> PlaceStorageItem(StorageItem storageItem)
    {
        var storageItemVolume = storageItem.Product.Dimensions.Volume;

        foreach (var cell in _cells)
        {
            if(cell.RemainingVolume < storageItemVolume)
            {
                continue;
            }

            var storageItemPlacementResult = cell.TryPlaceStorageItem(storageItem);

            if(storageItemPlacementResult.IsSuccess)
            {
                storageItem.AssignToStorageCell(cell);
                return Result<StorageCell>.Success(cell);
            }
        }

        return new Error("Storage.PlaceStorageItem", "Can not place storage item to store. ");
    }

    public Result PlaceStorageItem(StorageItem storageItem, StorageCell cell)
    {
        var storageItemPlacementResult = cell.TryPlaceStorageItem(storageItem);
        if(storageItemPlacementResult.IsFailure)
        {
            return new Error(
                "Storage.PlaceStorageItem", 
                "Can not place storage item to store.", 
                storageItemPlacementResult.Error);
        }

        storageItem.AssignToStorageCell(cell);

        return Result.Success();
    }

}
