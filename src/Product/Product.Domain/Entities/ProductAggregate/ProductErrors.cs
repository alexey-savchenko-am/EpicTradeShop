using SharedKernel.Output;

namespace Product.Domain.Entities.ProductAggregate;

public static class ProductErrors
{
    public static Error CategoryNameIsNullOrEmpty => new("Category.Create", "Category name can not be an empty string.");
    public static Error AttemptToSuspendDraftProduct => new("Product.ChangeStatus", "Attempted to suspend draft product");
    public static Error StockQuantityIsLessThanZero => new ("Product.SetStockQuantity", "Stock quantity is less than zero.");
    public static Error SetStockQuantityForDraftProduct => new("Product.SetStockQuantity", "Attempt to set stock quantity of draft product.");
    public static Error ProductPriceIsLessThanOrEqualToZero => new("Product.SetProductPrice", "New price is less than zero.");
    public static Error CanNotEditNonDraftProduct => new("Product.Edit", "Can not edit non-draft product.");

}

