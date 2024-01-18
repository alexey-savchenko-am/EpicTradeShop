using AppCommon.Cqrs;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Application.Product.Commands.AddImage;

public record AddImageCommand(
    BaseProduct.ID ProductId, 
    string ImageNameWithExtension,
    Uri ImageLink, 
    byte[] ImageData) : ICommand;

