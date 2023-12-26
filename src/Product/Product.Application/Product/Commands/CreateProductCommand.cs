using AppCommon.Cqrs;

namespace Product.Application.Product.Commands;

public sealed record CreateProductCommand 
    : ICommand<Guid>
{
    public CreateProductCommand(
        string name, 
        decimal price, 
        int length,
        int width,
        int height,
        int weight,
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
    public int Length { get; }
    public int Width { get; }
    public int Height { get; }
    public int Weight { get; }
    public List<string> Categories { get; } = new();
}
