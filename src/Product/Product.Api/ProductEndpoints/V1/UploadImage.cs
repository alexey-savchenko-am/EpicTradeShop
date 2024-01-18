using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints;
using Product.Application.Product.Commands.AddImage;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using System.Net;

namespace Product.Api.ProductEndpoints.V1;

public class UploadImage
    : IVersionedEndpoint<IResult, AddImageCommand>
{
    private readonly ISender _sender;

    public UploadImage(ISender sender)
    {
        _sender = sender;
    }

    public void ConfigureRoute(IEndpointRouteBuilder app, ApiVersionSet versions)
    {
        app.MapPost("api/v{version:apiVersion}/products/{productId}/images/{fileNameWithExtension}",
            async (HttpContext context, [FromRoute] Guid productId, [FromRoute] string fileNameWithExtension, [FromForm] IFormFile file) =>
            {
                if (file is null || file.Length == 0)
                {
                    return Results.BadRequest(Result.Failure(new Error("UploadImage", "File is null or empty.")));
                }

                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                return await HandleAsync(new AddImageCommand(
                        new BaseProduct.ID(productId),
                        fileNameWithExtension,
                        new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}"),
                        fileBytes));
            })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Upload an image of the product.")
            .WithTags("ProductEndpoint")
            .WithApiVersionSet(versions)
            .MapToApiVersion(1);
    }

    public async Task<IResult> HandleAsync(AddImageCommand request)
    {
        var result = await _sender.Send(request);  

        if(result.IsSuccess)
        {
            return Results.Created(request.ImageLink, result);
        }

        if (result.Error is HttpCodeError httpCodeError)
        {
            switch (httpCodeError.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return Results.NotFound(result);
                default:
                    return Results.BadRequest(result);
            }
        }
        
        return Results.BadRequest(result);
    }
}
