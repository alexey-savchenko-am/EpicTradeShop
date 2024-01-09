using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints;
using Product.Application.Product.Queries.GetById;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;

namespace Product.Api.ProductEndpoints.V1;

public class GetById
    : IVersionedEndpoint<IResult, GetProductByIdQuery>
{
    private readonly ISender _sender;

    public GetById(ISender sender)
    {
        _sender = sender;
    }

    public void ConfigureRoute(IEndpointRouteBuilder app, ApiVersionSet versions)
    {
        app.MapGet("api/v{version:apiVersion}/products/{productId}",
            async ([FromRoute] Guid productId)
                => await HandleAsync(new GetProductByIdQuery(new BaseProduct.ID(productId))))
          .Produces<Result<ProductResponse>>()
          .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .WithDescription("Gets product by its id.")
          .WithTags("ProductEndpoint")
          .WithApiVersionSet(versions)
          .MapToApiVersion(1);
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
