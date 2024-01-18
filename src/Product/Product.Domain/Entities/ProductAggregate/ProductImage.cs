using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public sealed class ProductImage
    : GuidKeyEntity
{
    public BaseProduct.ID ProductId { get; }
    public BaseProduct Product { get; }
    public string Name { get; }
    public string Link { get; }
    public byte[] Data { get; }

    private ProductImage()
        : base(new ID(Guid.NewGuid()))
    { }

    private ProductImage(string name, string link, byte[] data)
        : base(new ID(Guid.NewGuid()))
    {
        Name = name;
        Link = link;
        Data = data;
    }

    public static Result<ProductImage> Create(string name, Uri link, byte[] data)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            return new Error("ProductImage.Create", "Image name should not be empty.");
        }

        if (link is null)
        {
            return new Error("ProductImage.Create", "Image link should not be empty.");
        }

        if(data is null || data.Length == 0)
        {
            return new Error("ProductImage.Create", "Image data should not be empty.");
        }

        return new ProductImage(name, link.AbsoluteUri, data);
    }
}
