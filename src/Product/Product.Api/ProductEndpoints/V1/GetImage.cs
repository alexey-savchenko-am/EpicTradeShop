using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints;
using Product.Application.Product.Queries.GetImage;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Api.ProductEndpoints.V1;

public class GetImage
    : IVersionedEndpoint<IResult, GetImageQuery>
{
    private readonly ISender _sender;

    private static readonly Dictionary<string, string> MimeTypes = new()
    {
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" }
    };

    public GetImage(ISender sender)
    {
        _sender = sender;
    }

    public void ConfigureRoute(IEndpointRouteBuilder app, ApiVersionSet versions)
    {
        app.MapGet("api/v{version:apiVersion}/products/{productId}/images/{fileNameWithExtension}",
            async ([FromRoute] Guid productId, [FromRoute] string fileNameWithExtension)
                => await HandleAsync(new GetImageQuery(
                    new BaseProduct.ID(productId), 
                    fileNameWithExtension)))
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Upload an image of the product.")
            .WithTags("ProductEndpoint")
            .WithApiVersionSet(versions)
            .MapToApiVersion(1);
    }

    public async Task<IResult> HandleAsync(GetImageQuery request)
    {
        var result = await _sender.Send(request);

        if(result.IsFailure)
        {
            return Results.NotFound(result);
        }

        string fileExtensionWithDot = Path.GetExtension(request.ImageNameWithExtension);

        if (MimeTypes.TryGetValue(fileExtensionWithDot, out var mimeType))
        {
            return Results.File(result.Value.ImageData, mimeType);
        }
        else
        {
            return Results.File(result.Value.ImageData, "application/octet");
        }
    }
}
