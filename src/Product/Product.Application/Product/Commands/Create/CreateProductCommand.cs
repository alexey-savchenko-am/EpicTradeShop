using AppCommon.Cqrs;
using Product.Domain.Entities.ProductAggregate;

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
        int length,
        int width,
        int height,
        int weight,
        List<string> categories)
        : base(name, description, brand, model, price, length, width, height, weight, categories)
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
        int length,
        int width,
        int height,
        int weight,
        List<string> categories)
    {
        Name = name;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
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
    public int Length { get; }
    public int Width { get; }
    public int Height { get; }
    public int Weight { get; }
    public List<string> Categories { get; } = new();
}
