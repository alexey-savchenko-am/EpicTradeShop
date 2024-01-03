using SharedKernel.Enums;
using SharedKernel.Output;

namespace SharedKernel.ValueObjects;

public sealed class BrandModel
    : ValueObject
{
    public Brand Brand { get; }
    public string Model { get; }

    private BrandModel() { }

    public BrandModel(Brand brand, string model)
    {
        Brand = brand;
        Model = model;
    }

    public static Result<BrandModel> Create(Brand brand, string model)
    {
        if(string.IsNullOrWhiteSpace(model))
        {
            return new Error("BrandModel.Create", "Model should not be empty.");
        }
        return new BrandModel(brand, model);    
    }

    public static Result<BrandModel> Create(string brand, string model)
    {
        if (string.IsNullOrWhiteSpace(brand))
        {
            return new Error("BrandModel.Create", "Model should not be empty.");
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            return new Error("BrandModel.Create", "Model should not be empty.");
        }

        if(!Enum.TryParse<Brand>(brand, ignoreCase: true, out var brandEnum))
        {
            return new Error("BrandModel.Create", $"Unknown brand {brand}");
        }

        return new BrandModel(brandEnum, model);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Brand;
        yield return Model;
    }
}
