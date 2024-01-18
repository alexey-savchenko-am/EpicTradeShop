namespace Product.Application.Responses;

public sealed class LaptopProductResponse
    : ProductResponse
{
    public LaptopProductResponse(
        string id,
        string type,
        string status,
        int stockQuantity,
        string name,
        string description,
        string brand,
        string model,
        decimal? price,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        string color,
        string material,
        string operatingSystem,
        string processorBrand,
        string processorModel,
        decimal processorFrequencyGgc, 
        int processorCoreCount,
        int processorThreadCount,
        string graphicsControllerType,
        string graphicsBrand,
        string graphicsModel,
        int videoMemoryVolumeMb,
        decimal screenDiagonalInch,
        int screenWidthPx,
        int screenHeightPx,
        int refreshRateGc,
        int viewingAngleDeg,
        string ramType,
        int ramVolumeGb,
        decimal ramFrequencyMgc,
        bool ramIsUpgradeable,
        string storageType,
        int storageVolumeGb,
        bool storageIsUpgradeable,
        string batteryType,
        int batteryCellCount,
        int batteryCapacityWh,
        int batteryMaxWorktimeHrs)
            :base(id, type, status, stockQuantity, name, 
                description, brand, model, price, length, 
                width, height, weight, color, material)
    {
        OperatingSystem = operatingSystem;
        ProcessorBrand = processorBrand;
        ProcessorModel = processorModel;
        ProcessorFrequencyGgc = processorFrequencyGgc;
        ProcessorCoreCount = processorCoreCount;
        ProcessorThreadCount = processorThreadCount;
        GraphicsControllerType = graphicsControllerType;
        GraphicsBrand = graphicsBrand;
        GraphicsModel = graphicsModel;
        VideoMemoryVolumeMb = videoMemoryVolumeMb;
        ScreenDiagonalInch = screenDiagonalInch;
        ScreenWidthPx = screenWidthPx;
        ScreenHeightPx = screenHeightPx;
        RefreshRateGc = refreshRateGc;
        ViewingAngleDeg = viewingAngleDeg;
        RamType = ramType;
        RamVolumeGb = ramVolumeGb;
        RamFrequencyMgc = ramFrequencyMgc;
        RamIsUpgradeable = ramIsUpgradeable;
        StorageType = storageType;
        StorageVolumeGb = storageVolumeGb;
        StorageIsUpgradeable = storageIsUpgradeable;
        BatteryType = batteryType;
        BatteryCellCount = batteryCellCount;
        BatteryCapacityWh = batteryCapacityWh;
        BatteryMaxWorktimeHrs = batteryMaxWorktimeHrs;
    }

    public string OperatingSystem { get; }
    public string ProcessorBrand { get; }
    public string ProcessorModel { get; }
    public decimal ProcessorFrequencyGgc { get; }
    public int ProcessorCoreCount { get; }
    public int ProcessorThreadCount { get; }
    public string GraphicsControllerType { get; }
    public string GraphicsBrand { get; }
    public string GraphicsModel { get; }
    public int VideoMemoryVolumeMb { get; }
    public decimal ScreenDiagonalInch { get; }
    public int ScreenWidthPx { get; }
    public int ScreenHeightPx { get; }
    public int RefreshRateGc { get; }
    public int ViewingAngleDeg { get; }
    public string RamType { get; }
    public int RamVolumeGb { get; }
    public decimal RamFrequencyMgc { get; }
    public bool RamIsUpgradeable { get; }
    public string StorageType { get; }
    public int StorageVolumeGb { get; }
    public bool StorageIsUpgradeable { get; }
    public string BatteryType { get; }
    public int BatteryCellCount { get; }
    public int BatteryCapacityWh { get; }
    public int BatteryMaxWorktimeHrs { get; }
}