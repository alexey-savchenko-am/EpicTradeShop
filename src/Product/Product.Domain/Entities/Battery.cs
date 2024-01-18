
using Product.Domain.Enums;
using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities;

public class Battery
    : ValueObject
{
    public BatteryType BatteryType { get; }
    public int CellCount { get; }
    public int CapacityWh { get; }
    public int? MaxWorktimeHrs { get; }

    private Battery(BatteryType batteryType, int cellCount, int capacityWh, int? maxWorktimeHrs)
    {
        BatteryType = batteryType;
        CellCount = cellCount;
        CapacityWh = capacityWh;
        MaxWorktimeHrs = maxWorktimeHrs;
    }

    public static Result<Battery> Create(BatteryType batteryType, int cellCount, int capacityWh, int? maxWorktimeHrs)
    {
        return new Battery(batteryType, cellCount, capacityWh, maxWorktimeHrs);
    }

    public static Result<Battery> Create(string batteryTypeString, int cellCount, int capacityWh, int? maxWorktimeHrs)
    {
        if(!Enum.TryParse<BatteryType>(batteryTypeString, out var batteryType))
        {
            return new Error("Battery.Create", $"Unknown battery type {batteryTypeString}.");
        }

        return Create(batteryType, cellCount, capacityWh, maxWorktimeHrs);
    }


    public override IEnumerable<object> GetAtomicValues()
    {
        yield return BatteryType;
        yield return CellCount;
        yield return CapacityWh;
    }
}
