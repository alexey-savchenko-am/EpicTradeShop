using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Responses;

public sealed record LaptopProductResponse
    : ProductResponse
{
    public LaptopProductResponse(
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
        int weight,
        string processorBrand,
        string processorModel)
            :base(id, type, status, stockQuantity, name, description, brand, model, price, length, width, height, weight)
    {
        ProcessorBrand = processorBrand;
        ProcessorModel = processorModel;
    }

    public string ProcessorBrand { get; }
    public string ProcessorModel { get; }

}