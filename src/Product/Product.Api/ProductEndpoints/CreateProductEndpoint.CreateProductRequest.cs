using Presentation;
using Product.Application.Product.Commands;

namespace Product.Api.ProductEndpoints;

public class CreateProductRequest
    : Request<CreateProductCommand, Guid>
{
}
