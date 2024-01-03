using SharedKernel.Enums;
using SharedKernel.Output;

namespace SharedKernel.ValueObjects;

public sealed class Money
    : ValueObject
{

    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money() { }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, Currency currency)
    {
        if(amount <= 0)
        {
            return new Error("Money.Create", "Amount shoud be greater than zero.");
        }

        return new Money(amount, currency);
    }

    public static Result<Money> CreateUsd(decimal amount)
        => Create(amount, Currency.USD);
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
