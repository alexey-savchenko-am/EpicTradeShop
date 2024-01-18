using Product.Application.Requests.Common;
using Product.Domain.Enums;

namespace Product.Application.Requests;

public record LaptopProductRequest
{
    public LaptopProductRequest(
        ProductOperatingSystem operatingSystem,
        ProcessorRequest processor,
        GraphicsRequest graphics,
        DisplayRequest display,
        RamRequest ram,
        StorageRequest storage,
        BatteryRequest battery)
    {
        OperatingSystem = operatingSystem;
        Processor = processor;
        Graphics = graphics;
        Display = display;
        Ram = ram;
        Storage = storage;
        Battery = battery;
    }

    public ProductOperatingSystem OperatingSystem { get; }
    public ProcessorRequest Processor { get; }
    public GraphicsRequest Graphics { get; }
    public DisplayRequest Display{ get; }
    public RamRequest Ram { get; }
    public StorageRequest Storage { get; }
    public BatteryRequest Battery { get; }
}
