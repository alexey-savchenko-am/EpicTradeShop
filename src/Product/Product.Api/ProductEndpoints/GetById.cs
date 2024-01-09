using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;
using Product.Application.Product.Queries.GetById;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints;

public class GetById
    : IEndpoint<IResult, GetProductByIdQuery>
{
    private readonly ISender _sender;

    public GetById(ISender sender)
    {
        _sender = sender;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{productId}",
        async ([FromQuery] Guid productId)
                => await HandleAsync(new GetProductByIdQuery(new BaseProduct.ID(productId))))
          .Produces<Result<ProductResponse>>()
          .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .WithMetadata("Gets product by its id.")
          .WithTags("ProductEndpoint");
    }


    public async Task<IResult> HandleAsync(GetProductByIdQuery request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
        {
            return Results.BadRequest(result);
        }

        if (result.Value is null)
        {
            return Results.NoContent();
        }

        return Results.Ok(result);
    }
}
