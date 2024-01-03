namespace Product.Application.Requests.Common;

public record ProcessorRequest(
    string Brand, 
    string Model, 
    decimal FrequencyGgc, 
    int CoreCount, 
    int ThreadCount);

