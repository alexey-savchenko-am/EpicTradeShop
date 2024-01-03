namespace Product.Application.Requests.Common;

public record RamRequest(
    string Type, 
    int Volume, 
    decimal FrequencyMgc, 
    bool IsUpgradeable);

