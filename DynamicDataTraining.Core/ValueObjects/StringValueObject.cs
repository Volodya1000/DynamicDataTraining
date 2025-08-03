using System.Text.RegularExpressions;

namespace DynamicDataTraining.Core.ValueObjects;

public abstract record StringValueObject
{
    public string Value { get; init; }
    public int MinLength { get; }
    public int MaxLength { get; }

    protected Regex ValidationRegex { get; }

    protected StringValueObject(string value, int minLength, int maxLength)
    {
        MinLength = minLength;
        MaxLength = maxLength;
        ValidationRegex = CreateDefaultRegex(minLength, maxLength);

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty", nameof(value));

        if (value.Length < MinLength || value.Length > MaxLength)
            throw new ArgumentException($"Value must be between {MinLength} and {MaxLength} characters", nameof(value));

        if (!ValidationRegex.IsMatch(value))
            throw new ArgumentException("Value does not match required pattern", nameof(value));

        Value = value;
    }

    private static Regex CreateDefaultRegex(int minLength, int maxLength) => new(
        $@"^[\p{{L}}\p{{M}}\p{{N}}]{{{minLength},{maxLength}}}$",
        RegexOptions.Singleline | RegexOptions.Compiled);

    public virtual bool Equals(StringValueObject? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        if (GetType() != other.GetType()) return false;

        return string.Equals(Value, other.Value, StringComparison.Ordinal)
            && MinLength == other.MinLength
            && MaxLength == other.MaxLength;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Value, MinLength, MaxLength);
    }
}
