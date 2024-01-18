using SharedKernel;
using SharedKernel.Output;
using Stock.Domain.Entities.StockItemAggregate;
using Stock.Domain.Entities.StorageAggregate;

namespace Stock.Domain.Entities.StorageItemAggregate;

public class StorageItem
    : AggregateRoot
{
    public Storage.ID StorageId { get; }
    public Product.ID ProductId { get; }
    public Product Product { get; }
    public string? Sku { get; private set; }
    public StorageCell.ID? CellId { get; private set; }
    public StorageCell? Cell { get; private set; }
    public bool IsPlaced => CellId is not null;

    private StorageItem(Storage.ID storageId, Product.ID productId)
        : base(new ID(Guid.NewGuid()))
    {
        StorageId = storageId;
        ProductId = productId;
    }

    public static Result<StorageItem> Create(Storage storage, Product product)
    {
        return new StorageItem(storage.Id, product.Id);
    }

    public void AssignToStorageCell(StorageCell cell)
    {
        CellId = cell.Id;
        Cell = cell;
    }
}
