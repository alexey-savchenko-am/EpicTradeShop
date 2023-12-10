using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public class Category
    : ValueObject
{
    public string Name { get; }

    private Category(string name)
    {
        this.Name = name;
    }

    public static Result<Category> Create(string name)
    {
        return new Category(name);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return this.Name;
    }
}
