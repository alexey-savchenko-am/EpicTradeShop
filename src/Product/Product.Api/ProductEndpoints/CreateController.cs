using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Product.Commands.Create;
using Product.Application.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Product.Api.ProductEndpoints;

public class CreateController
    : Controller
{
    private readonly ISender _sender;

    public CreateController(ISender sender)
	{
        _sender = sender;
    }

    [Route("api/products/laptop")]
    [HttpPost]
    [SwaggerOperation("CreateLaptopProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLaptopProduct(
        [FromBody]CreateProductCommand<LaptopProductRequest> createLaptopProductCommand)
    {
        var result = await _sender.Send(createLaptopProductCommand);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }

        return Created(new Uri($"api/products/{result.Value.Key}"), result);
    }
}
