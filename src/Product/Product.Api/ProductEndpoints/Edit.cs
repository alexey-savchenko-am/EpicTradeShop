using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;
using Product.Application.Product.Commands;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints;

public sealed class Edit
    : IEndpoint<IResult, UpsertProductCommand>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/product",

        [SwaggerOperation("EditProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        async ([FromBody] UpsertProductCommand request) => await HandleAsync(request))
            .Produces<Result<ProductAggregate.ID>>()
            .WithMetadata("Edit an existed product");
    }

    public Task<IResult> HandleAsync(UpsertProductCommand request)
    {
        throw new NotImplementedException();
    }
}
