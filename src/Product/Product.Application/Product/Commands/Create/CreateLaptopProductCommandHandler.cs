using AppCommon.Cqrs;
using AppCommon.Persistence;
using Product.Application.Abstract;
using Product.Application.Requests;
using Product.Domain.Entities;
using Product.Domain.Entities.ProductAggregate;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Application.Product.Commands.Create;

public class CreateLaptopProductCommandHandler
    : BaseCreateProductCommandHandler<LaptopProductRequest, LaptopProduct>
{
    public CreateLaptopProductCommandHandler(IProductRepository<LaptopProduct> laptopProductRepository, IUnitOfWork unitOfWork)
        : base(laptopProductRepository, unitOfWork)
    {}

    protected override Result<LaptopProduct> CreateConcreteProduct(
        LaptopProductRequest request, 
        ProductEntities productEntities)
    {
        var processorBrandModelResult = BrandModel.Create(
            request.Processor.Brand, 
            request.Processor.Model);
        
        var processorResult = Processor.Create(
            processorBrandModelResult, 
            request.Processor.FrequencyGgc, 
            request.Processor.CoreCount, 
            request.Processor.ThreadCount);

        var graphicsBrandModelResult = BrandModel.Create(
            request.Graphics.VideoControllerBrand, 
            request.Graphics.VideoControllerModel);

        Result<Graphics> graphicsResult;
        if (request.Graphics.IsDiscrete)
        {
            graphicsResult = Graphics.CreateDiscrete(graphicsBrandModelResult, request.Graphics.VolumeMb);
        }
        else
        {
            graphicsResult = Graphics.CreateIntegrated(graphicsBrandModelResult, request.Graphics.VolumeMb);

        }

        var screenResolutionResult = ScreenResolution.Create(
            request.Display.widthPixel, 
            request.Display.heightPixel);

        var displayResult = Display.Create(
            request.Display.diagonalInch,
            screenResolutionResult,
            request.Display.refreshRateGc,
            request.Display.viewingAngleDeg);

        var ramResult = Ram.Create(
            request.Ram.Type, 
            request.Ram.Volume, 
            request.Ram.FrequencyMgc, 
            request.Ram.IsUpgradeable);


        Result<StorageDevice> storageDeviceResult;

        if(request.Storage.IsSsd)
        {
            storageDeviceResult = StorageDevice.CreateSsd(request.Storage.VolumeGb, request.Storage.IsUpgradeable);
        } 
        else
        {
            storageDeviceResult = StorageDevice.CreateSsd(request.Storage.VolumeGb, request.Storage.IsUpgradeable);
        }

        var laptopProductResult = LaptopProduct.Create(
          productEntities.ProductDetails,
          productEntities.BrandModel,
          productEntities.DimensionsInfo,
          processorResult,
          graphicsResult,
          displayResult,
          ramResult,
          storageDeviceResult);

        return laptopProductResult;
    }
}
