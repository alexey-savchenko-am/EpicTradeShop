using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;
using Product.Application.Product.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints;

public sealed class CreateProductEndpoint
    : IEndpoint<IResult, CreateProductCommand>
{
    private readonly ISender _sender;

    public CreateProductEndpoint()
    {
        _sender = null;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product",
            [SwaggerOperation("CreateProduct")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            async ([FromBody] CreateProductCommand request) => await HandleAsync(request))
                .Produces<Guid>()
                .WithMetadata("Creates a new Product.");
    }

    public async Task<IResult> HandleAsync(CreateProductCommand request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error.Message);
        }

        return Results.Created($"/api/product/{result.Value}", result.Value);
    }
}
