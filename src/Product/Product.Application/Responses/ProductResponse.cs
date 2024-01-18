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
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        string color,
        string material)
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
        Color = color;
        Material = material;
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
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public decimal Weight { get; }
    public string Color { get; }
    public string Material { get; }

    public string DetailsUrl => $"/api/products/{Id}";

    public string GabaritesHwl => $"{Height} cm X {Width} cm X {Length} cm";
}
