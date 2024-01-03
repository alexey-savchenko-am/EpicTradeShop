using Product.Domain.Enums;
using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities;

public class Ram
    : ValueObject
{
    public RamType Type { get; }
    public int VolumeGb { get; }
    public decimal FrequencyMgc { get; }
    public bool IsUpgradable { get; }

    private static Error RamError(Error innerError) 
        => new("Ram.Create", "Can not create RAM", innerError);

    private Ram() { }

    public Ram(RamType type, int volumeGb, decimal frequencyMgc, bool isUpgradable)
    {
        Type = type;
        VolumeGb = volumeGb;
        FrequencyMgc = frequencyMgc;
        IsUpgradable = isUpgradable;
    }

    public static Result<Ram> Create(RamType type, int volumeGb, decimal frequencyMgc, bool isUpgradeable = false)
    {
        return new Ram(type, volumeGb, frequencyMgc, isUpgradeable);
    }

    public static Result<Ram> Create(string type, int volumeGb, decimal frequencyMgc, bool isUpgradeable = false)
    {
        if(!Enum.TryParse<RamType>(type, ignoreCase: true, out var typeEnum))
        {
            return RamError(new Error("", $"Unknown RAM type {type}."));
        }
        return Create(typeEnum, volumeGb, frequencyMgc, isUpgradeable);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Type;
        yield return VolumeGb;
        yield return FrequencyMgc;
        yield return IsUpgradable;
    }
}
