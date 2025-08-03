namespace DynamicDataTraining.Core.ValueObjects;

public sealed record Name : StringValueObject
{
    public const int MIN_LENGTH = 4;
    public const int MAX_LENGTH = 15;

    public Name(string value)
        : base(value, MIN_LENGTH, MAX_LENGTH)
    {
    }
}

