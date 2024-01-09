namespace Product.Application.Responses;

public class ProductResponse
    : BaseProductResponse
{
    public ProductResponse(
        string id, 
        string type, 
        string status, 
        int stockQuantity, 
        string name,
        string description,
        string brand,
        string model,
        decimal? price,
        int length,
        int width,
        int height,
        int weight)
    {
        Id = id;
        Type = type;
        Status = status;
        StockQuantity = stockQuantity;
        Name = name;
        Description = description;
        Brand = brand;
        Model = model;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
    }

    public string Id { get; }
    public string Type { get; }
    public string Status { get; }
    public int StockQuantity { get; }
    public string Name { get; }
    public string Description { get; }
    public string Brand { get; }
    public string Model { get; }
    public decimal? Price { get; }
    public int Length { get; }
    public int Width { get; }
    public int Height { get; }
    public int Weight { get; }

    public string DetailsUrl => $"/api/products/{Id}";

    public string GabaritesHwl => $"{Height} cm X {Width} cm X {Length} cm";
}
