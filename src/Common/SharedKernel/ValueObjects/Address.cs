using SharedKernel.Output;

namespace SharedKernel.ValueObjects;

public sealed class Address
    : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    private Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public static Result<Address> Create(string street, string city, string state, string postalCode, string country)
    {
        return new Address(street, city, state, postalCode, country);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Street; 
        yield return City; 
        yield return State; 
        yield return PostalCode; 
        yield return Country;
    }
}
