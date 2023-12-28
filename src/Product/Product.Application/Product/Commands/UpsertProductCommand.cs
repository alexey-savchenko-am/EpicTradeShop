using AppCommon.Cqrs;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Product.Commands;

public sealed record UpsertProductCommand 
    : ICommand<ProductAggregate.ID>
{
    public UpsertProductCommand(
        ProductAggregate.ID? productId,
        string name, 
        decimal? price, 
        int length,
        int width,
        int height,
        int weight,
        List<string> categories, 
        string description = "")
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Description = description;
        Categories = categories;
    }

    public ProductAggregate.ID? ProductId { get; }
    public string Name { get; }
    public string? Description { get; }
    public decimal? Price { get; }
    public int Length { get; }
    public int Width { get; }
    public int Height { get; }
    public int Weight { get; }
    public List<string> Categories { get; } = new();
}
