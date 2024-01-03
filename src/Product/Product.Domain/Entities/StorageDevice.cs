using Product.Domain.Enums;
using SharedKernel;
using SharedKernel.Output;

namespace Product.Domain.Entities;

public sealed class StorageDevice
    : ValueObject
{
    public StorageType StorageType { get; }
    public int VolumeGb { get; }
    public bool IsUpgradeable { get; }

    private StorageDevice() { }

    public StorageDevice(StorageType storageType, int volumeGb, bool isUpgradeable)
    {
        StorageType = storageType;
        VolumeGb = volumeGb;
        IsUpgradeable = isUpgradeable;
    }

    public static Result<StorageDevice> Create(StorageType storageType, int volumeGb, bool isUpgradeable)
    {
        return new StorageDevice(storageType, volumeGb, isUpgradeable);
    }

    public static Result<StorageDevice> CreateHdd(int volumeGb, bool isUpgradeable)
        => Create(StorageType.HDD, volumeGb, isUpgradeable);

    public static Result<StorageDevice> CreateSsd(int volumeGb, bool isUpgradeable)
        => Create(StorageType.SSD, volumeGb, isUpgradeable);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return StorageType;
        yield return VolumeGb;
        yield return IsUpgradeable;
    }
}
