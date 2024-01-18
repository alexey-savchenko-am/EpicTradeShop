namespace Product.Application.Requests.Common;

public sealed record BatteryRequest(
    string Type,
    int CellCount, 
    int CapacityWh, 
    int? MaxWorktimeHrs);
