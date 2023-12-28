using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;
using Product.Application.Product.Commands;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints;

public sealed class Create
    : IEndpoint<IResult, UpsertProductCommand>
{
    private readonly ISender _sender;

    public Create(ISender sender)
    {
        _sender = sender;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product",

            [SwaggerOperation("CreateProduct")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            async ([FromBody] UpsertProductCommand request) => await HandleAsync(request))
                .Produces<Result<ProductAggregate.ID>>()
                .WithMetadata("Creates a new Product.");
    }

    public async Task<IResult> HandleAsync(UpsertProductCommand request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
        {
            return Results.BadRequest(result);
        }

        return Results.Created($"/api/product/{result.Value}", result);
    }
}
