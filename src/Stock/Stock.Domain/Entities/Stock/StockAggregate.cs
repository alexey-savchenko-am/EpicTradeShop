using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;
using Stock.Domain.Entities.StockItem;

namespace Stock.Domain.Entities.Stock;

public sealed class StockAggregate
    : AggregateRoot
{
    private readonly List<Cell> _cells = new();

    public string Name { get; }
    public Address Address { get; }
    public IReadOnlyCollection<Cell> Cells => _cells.AsReadOnly();

    private StockAggregate(string name, Address address, ICollection<Cell> locations)
        : base(new ID())
    {
        Name = name;
        Address = address;
        _cells.AddRange(locations);
    }

    public Result AddCells(params Cell[] cells)
    {
        if (!cells.Any())
        {
            return Result.Success();
        }


        _cells.AddRange(cells); 
        return Result.Success();
    }
}
