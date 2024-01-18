namespace Product.Application.Requests.Common;

public record StorageRequest(
    bool IsSsd, 
    int VolumeGb, 
    bool IsUpgradeable);

