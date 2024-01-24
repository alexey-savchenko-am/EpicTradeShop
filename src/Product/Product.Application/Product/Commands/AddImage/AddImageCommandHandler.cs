using AppCommon.Cqrs;
using AppCommon.Persistence;
using Microsoft.AspNetCore.Hosting;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using System.Net;

namespace Product.Application.Product.Commands.AddImage;

public class AddImageCommandHandler
    : ICommandHandler<AddImageCommand>
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductRepository<BaseProduct> _productRepository;
    private readonly ISession _unitOfWork;
    private static HttpCodeError CanNotAddImageError(Error innerError, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new HttpCodeError("AddImage.Handle", "Can not add image.", statusCode, innerError);

    private static string StaticImageFolderName = "images";

    public AddImageCommandHandler(
        IWebHostEnvironment webHostEnvironment,
        IProductRepository<BaseProduct> productRepository,
        ISession unitOfWork)
    {
        _webHostEnvironment = webHostEnvironment;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken);

        if (product is null) 
        {
            return CanNotAddImageError(
                new Error("AddImage.Handle", $"Can not find product by id ${request.ProductId}."), 
                HttpStatusCode.NotFound);
        }

        try
        {
            await SaveImageFileAsync(request);
        }
        catch (UnauthorizedAccessException ex)
        {
            return CanNotAddImageError(new Error("AddImage.Handle", $"Unauthorize access to file system while creating an image. {ex.Message}"));
        }
        catch (IOException ex)
        {
            return CanNotAddImageError(new Error("AddImage.Handle", $"Can not create an image file because of IO problems. {ex.Message}"));
        }

        var imageResult = ProductImage.Create(request.ImageNameWithExtension, request.ImageLink, request.ImageData);

        if(imageResult.IsFailure)
        {
            return CanNotAddImageError(imageResult.Error);
        }

        var addImageResult = product.AddImage(imageResult);

        if (addImageResult.IsFailure)
        {
            return CanNotAddImageError(addImageResult.Error);
        }
        
        await _unitOfWork.StoreAsync(cancellationToken);

        return Result.Success();
    }

    private async Task SaveImageFileAsync(AddImageCommand request)
    {
        string imageFolderPath = GetImageFolderPath(request.ProductId.Key.ToString());

        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }

        string filePath = Path.Combine(imageFolderPath, request.ImageNameWithExtension);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        fileStream.Write(request.ImageData, 0, request.ImageData.Length);
    }

    private string GetImageFolderPath(string productIdAsString) =>
         Path.Combine(
            _webHostEnvironment.WebRootPath,
            StaticImageFolderName,
            productIdAsString);
}
