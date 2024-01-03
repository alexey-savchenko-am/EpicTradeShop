using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities;

public class ProductDetails
    : ValueObject
{
    public string Name { get; }
    public string Description { get; }

    private ProductDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<ProductDetails> Create(string name, string description)
    {
        if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            return new Error("ProductDetails.Create", "Product name and description should not be empty.");
        }
        return new ProductDetails(name, description);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Description;
    }
}
