using Product.Application.Requests.Common;

namespace Product.Application.Requests;

public record LaptopProductRequest
{
    public LaptopProductRequest(
        ProcessorRequest processor,
        GraphicsRequest graphics,
        DisplayRequest display,
        RamRequest ram,
        StorageRequest storage)
    {
        Processor = processor;
        Graphics = graphics;
        Display = display;
        Ram = ram;
        Storage = storage;
    }

    public ProcessorRequest Processor { get; }
    public GraphicsRequest Graphics { get; }
    public DisplayRequest Display{ get; }
    public RamRequest Ram { get; }
    public StorageRequest Storage { get; }
}
