namespace DynamicDataTraining.Core.ValueObjects;

public sealed class FullName 
{
    public Name FirstName { get; }
    public Name LastName { get; }
    public Name Surname { get; }

    public FullName(Name firstName, Name lastName, Name surname)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Surname = surname ?? throw new ArgumentNullException(nameof(surname));
    }

    public override string ToString() =>
        $"{Surname.Value} {FirstName.Value} {LastName.Value}";

    public bool Equals(FullName? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;

        return FirstName.Equals(other.FirstName)
            && LastName.Equals(other.LastName)
            && Surname.Equals(other.Surname);
    }

    public override bool Equals(object? obj) => Equals(obj as FullName);

    public override int GetHashCode() =>
        HashCode.Combine(FirstName, LastName, Surname);
}
