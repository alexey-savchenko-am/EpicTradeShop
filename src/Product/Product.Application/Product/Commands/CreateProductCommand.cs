using AppCommon.Cqrs;

namespace Product.Application.Product.Commands;

public sealed record CreateProductCommand 
    : ICommand<Guid>
{
    public CreateProductCommand(
        string name, 
        decimal price, 
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        List<string> categories, 
        string description = "")
    {
        Name = name;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Description = description;
        Categories = categories;
    }

    public string Name { get; }
    public string? Description { get; }
    public decimal Price { get; }
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public decimal Weight { get; }
    public List<string> Categories { get; } = new();
}
