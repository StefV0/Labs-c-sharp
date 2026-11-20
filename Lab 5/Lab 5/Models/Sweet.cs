namespace Lab_5.Models;

public abstract class Sweet : IComparable<Sweet>
{
    private double _weight;
    private double _sugarPercentage;

    public string Name { get; }
    public string Manufacturer { get; }

    public double Weight
    {
        get => _weight;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Weight must be greater than zero.");
            }
            _weight = value;
        }
    }

    public double SugarPercentage
    {
        get => _sugarPercentage;
        set
        {
            if (value is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Sugar percentage must be between 0 and 100.");
            }
            _sugarPercentage = value;
        }
    }

    public double TotalSugarWeight => Weight * (SugarPercentage / 100.0);

    protected Sweet(string name, double weight, double sugarPercentage, string manufacturer)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(manufacturer))
        {
            throw new ArgumentException("Manufacturer cannot be null or empty.", nameof(manufacturer));
        }

        Name = name;
        Manufacturer = manufacturer;
        Weight = weight;
        SugarPercentage = sugarPercentage;
    }

    public virtual int CompareTo(Sweet? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Weight.CompareTo(other.Weight);
    }

    public override string ToString()
    {
        return $"{Name} ({Manufacturer}) - Вес: {Weight:F1} г, Сахар: {SugarPercentage:F1}% ({TotalSugarWeight:F1} г)";
    }
}
