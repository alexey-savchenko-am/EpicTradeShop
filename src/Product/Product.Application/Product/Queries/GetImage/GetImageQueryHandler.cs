using AppCommon.Cqrs;
using Microsoft.AspNetCore.Hosting;
using Product.Application.Abstract;
using Product.Application.Responses;
using SharedKernel.Output;
using System.Net;

namespace Product.Application.Product.Queries.GetImage;

public class GetImageQueryHandler
    : IQueryHandler<GetImageQuery, GetProductImageResponse>
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductsQueryService _productQueryService;
    private static string StaticImageFolderName = "images";


    public GetImageQueryHandler(
        IWebHostEnvironment webHostEnvironment,
        IProductsQueryService productQueryService)
    {
        _webHostEnvironment = webHostEnvironment;
        _productQueryService = productQueryService;
    }

    public async Task<Result<GetProductImageResponse>> Handle(
        GetImageQuery request, CancellationToken cancellationToken)
    {
        var imageFolderRoot = GetImageFolderPath(request.ProductId.Key.ToString());
        
        if (!Directory.Exists(imageFolderRoot))
        {
            Directory.CreateDirectory(imageFolderRoot);
        }

        var imagePath = Path.Combine(imageFolderRoot, request.ImageNameWithExtension);

        byte[]? imageBytes = null;
        if (File.Exists(imagePath))
        {
            imageBytes = File.ReadAllBytes(imagePath);
            return new GetProductImageResponse(request.ImageNameWithExtension, imageBytes);
        }

        imageBytes = await _productQueryService
            .GetProductImageByIdAsync(request.ProductId, request.ImageNameWithExtension, cancellationToken);

        if (imageBytes is null || imageBytes.Length == 0)
        {
            return new HttpCodeError("GetImageQueryHandler.Handle", $"Can not get product image by product id {request.ProductId.Key}", HttpStatusCode.NotFound);
        }

        // the image is already stored in the database, but an appropriate file does not exist
        await using var fileStream = new FileStream(imagePath, FileMode.Create);
        fileStream.Write(imageBytes, 0, imageBytes.Length);

        return new GetProductImageResponse(request.ImageNameWithExtension, imageBytes);
    }


    private string GetImageFolderPath(string productIdAsString) =>
        Path.Combine(
                _webHostEnvironment.WebRootPath,
                StaticImageFolderName,
                productIdAsString);
}
