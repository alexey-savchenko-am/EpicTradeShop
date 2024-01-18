namespace Product.Application.Responses;

public sealed record GetProductImageResponse
{
    public GetProductImageResponse(
        string imageName, 
        byte[] imageData)
    {
        ImageName = imageName;
        ImageData = imageData;
    }

    public string ImageName { get; }
    public byte[] ImageData { get; }
}
