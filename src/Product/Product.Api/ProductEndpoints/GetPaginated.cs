using MediatR;
using MinimalApi.Endpoint;
using Product.Application.Product.Queries.GetAllPaginated;
using Product.Application.Responses;
using SharedKernel.Output;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints;


[SwaggerTag("Product")]
public class GetPaginated
    : IEndpoint<IResult, GetAllProductsPaginatedQuery>
{
    private readonly ISender _sender;

    public GetPaginated(ISender sender)
    {
        _sender = sender;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products",
        async (int page, int itemsPerPage)
                => await HandleAsync(new GetAllProductsPaginatedQuery(page, itemsPerPage)))
          .Produces<Result<PagedResponse<ProductResponse>>>()
          .Produces(StatusCodes.Status204NoContent)
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
          .WithMetadata("Retrieve a list of all products stored in the database, divided into pages of a specified size.")
          .WithTags("ProductEndpoint");
    }

    public async Task<IResult> HandleAsync(GetAllProductsPaginatedQuery request)
    {
        var result = await _sender.Send(request);

        if(result.IsFailure)
        {
            return Results.BadRequest(result);
        }

        if(!result.Value.Items.Any())
        {
            return Results.NoContent();
        }
        
        return Results.Ok(result);
    }
}
