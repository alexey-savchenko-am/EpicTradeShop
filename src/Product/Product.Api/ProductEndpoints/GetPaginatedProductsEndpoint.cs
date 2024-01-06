using MediatR;
using MinimalApi.Endpoint;
using Product.Application.Product.Queries.GetAllPaginated;
using Product.Application.Responses;

namespace Product.Api.ProductEndpoints;

public class GetPaginatedProductsEndpoint
    : IEndpoint<IResult, GetAllProductsPaginatedQuery>
{
    private readonly ISender _sender;

    public GetPaginatedProductsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products",
            async (int page, int itemsPerPage)
                => await HandleAsync(new GetAllProductsPaginatedQuery(page, itemsPerPage)))
          .Produces<PaginatedResult<ProductResponse>>()
          .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .WithMetadata("Retrieve a list of all products stored in the database, divided into pages of a specified size.");
    }

    public async Task<IResult> HandleAsync(GetAllProductsPaginatedQuery request)
    {
        var result = await _sender.Send(request);

        if(result.IsFailure)
        {
            return Results.BadRequest(result.Error.Message);
        }

        if(result.Value.Items.Count == 0)
        {
            return Results.NoContent();
        }
        
        return Results.Ok(result.Value);
    }
}
