using Asp.Versioning.Builder;
using MediatR;
using MinimalApi.Endpoint;
using Presentation.Endpoints;
using Product.Application.Product.Queries.GetAllPaginated;
using Product.Application.Product.Queries.GetById;
using Product.Application.Responses;
using SharedKernel.Output;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints.V1;

public class GetPaginated
    : IVersionedEndpoint<IResult, GetAllProductsPaginatedQuery>
{
    private readonly ISender _sender;

    public GetPaginated(ISender sender)
    {
        _sender = sender;
    }

    public void ConfigureRoute(IEndpointRouteBuilder app, ApiVersionSet versions)
    {
        app.MapGet("api/v{version:apiVersion}/products",
            async (int page, int itemsPerPage)
                => await HandleAsync(new GetAllProductsPaginatedQuery(page, itemsPerPage)))
          .Produces<Result<PagedResponse<ProductResponse>>>()
          .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .WithDescription("Retrieve a list of all products stored in the database, divided into pages of a specified size.")
          .WithTags("ProductEndpoint")
          .WithApiVersionSet(versions)
          .MapToApiVersion(1);
    }

    public async Task<IResult> HandleAsync(GetAllProductsPaginatedQuery request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
        {
            return Results.BadRequest(result);
        }

        if (!result.Value.Items.Any())
        {
            return Results.NoContent();
        }

        return Results.Ok(result);
    }
}
