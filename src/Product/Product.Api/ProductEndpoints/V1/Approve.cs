using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints;
using Product.Application.Product.Commands.Approve;
using Product.Application.Product.Queries.GetAllPaginated;
using Product.Application.Responses;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Api.ProductEndpoints.V1;

public class Approve
    : IVersionedEndpoint<IResult, ApproveProductCommand>
{
    private readonly ISender _sender;

    public Approve(ISender sender)
    {
        _sender = sender;
    }

    public void ConfigureRoute(IEndpointRouteBuilder app, ApiVersionSet versions)
    {
        app.MapPost("api/v{version:apiVersion}/products/{productId}/approve",
            async ([FromQuery]Guid productId, [FromBody]decimal? price)
                => await HandleAsync(new ApproveProductCommand(
                    new BaseProduct.ID(productId), 
                    price is null ? null : Money.CreateUsd(price.Value))))
          .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .WithDescription("Retrieve a list of all products stored in the database, divided into pages of a specified size.")
          .WithTags("ProductEndpoint")
          .WithApiVersionSet(versions)
          .MapToApiVersion(1);
    }

    public async Task<IResult> HandleAsync(ApproveProductCommand request)
    {
        var result = await _sender.Send(request);

        return Results.Ok(result);

    }
}
