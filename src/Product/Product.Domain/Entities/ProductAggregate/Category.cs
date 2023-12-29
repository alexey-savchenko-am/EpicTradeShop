using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public class Category
    : GuidKeyEntity
{

    private List<ProductAggregate> _products = new();

    public string Name { get; }
    public IReadOnlyCollection<ProductAggregate> Products => _products.AsReadOnly();

    private Category()
        : base(new ID(Guid.NewGuid()))
    { }

    private Category(string name)
        : base(new ID())
    {
        Name = name;
    }

    public static Result<Category> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return ProductErrors.CategoryNameIsNullOrEmpty;
        }

        return new Category(name);
    }
}
