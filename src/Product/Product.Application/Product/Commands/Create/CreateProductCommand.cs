using AppCommon.Cqrs;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Enums;

namespace Product.Application.Product.Commands.Create;

public class CreateProductCommand<TConcreteProduct>
    : CreateProductCommand
    where TConcreteProduct : class
{
    public CreateProductCommand(
        TConcreteProduct product,
        string name,
        string description,
        string brand,
        string model,
        decimal? price,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        Color color,
        Material material,
        List<string> categories)
        : base(name, description, brand, model, price, 
            length, width, height, weight, color, material, categories)
    {
        Product = product;
    }

    public TConcreteProduct Product { get; }
}

public class CreateProductCommand
    : ICommand<BaseProduct.ID>
{
    public CreateProductCommand(
        string name,
        string description,
        string brand,
        string model,
        decimal? price,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        Color color,
        Material material,
        List<string> categories)
    {
        Name = name;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Color = color;
        Material = material;
        Description = description;
        Brand = brand;
        Model = model;
        Categories = categories;
    }

    public string Name { get; }
    public string Description { get; }
    public string Brand { get; }
    public string Model { get; }
    public decimal? Price { get; }
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public decimal Weight { get; }
    public Color Color { get; }
    public Material Material { get; }
    public List<string> Categories { get; } = new();
}
