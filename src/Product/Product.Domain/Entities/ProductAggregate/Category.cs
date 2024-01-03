using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public class Category
    : GuidKeyEntity
{

    private List<BaseProduct> _products = new();

    public string Name { get; }
    public IReadOnlyCollection<BaseProduct> Products => _products.AsReadOnly();

    private Category()
        : base(new ID(Guid.NewGuid()))
    { }

    private Category(string name)
        : base(new ID(Guid.NewGuid()))
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
