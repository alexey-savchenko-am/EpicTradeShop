using SharedKernel;
using SharedKernel.Output;
using SharedKernel.ValueObjects;

namespace Product.Domain.Entities;

public sealed class Processor
    : ValueObject
{
    public BrandModel BrandModel { get; }
    public decimal FrequencyGgc { get; }
    public int CoreCount { get; }
    public int ThreadCount { get; }

    private Processor() {}

    public Processor(BrandModel brandModel, decimal frequencyGgc, int coreCount, int threadCount)
    {
        BrandModel= brandModel;
        FrequencyGgc = frequencyGgc;
        CoreCount = coreCount;
        ThreadCount = threadCount;
    }

    public static Result<Processor> Create(BrandModel brandModel, decimal frequencyGgc, int coreCount, int threadCount)
    {
        return new Processor(brandModel, frequencyGgc, coreCount, threadCount); 
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return BrandModel;
        yield return FrequencyGgc;
        yield return CoreCount;
        yield return ThreadCount;
    }
}
