using Lab_5.Enums;

namespace Lab_5.Models;

public class ChocolateCandy : Sweet
{
    private double _cocoaPercentage;

    public ChocolateType ChocolateType { get; }
    public bool HasFilling { get; }
    public string FillingType { get; }

    public double CocoaPercentage
    {
        get => _cocoaPercentage;
        set
        {
            if (value is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Cocoa percentage must be between 0 and 100.");
            }
            _cocoaPercentage = value;
        }
    }

    public ChocolateCandy(
        string name,
        double weight,
        double sugarPercentage,
        string manufacturer,
        ChocolateType chocolateType,
        double cocoaPercentage,
        bool hasFilling = false,
        string fillingType = "Без начинки")
        : base(name, weight, sugarPercentage, manufacturer)
    {
        ChocolateType = chocolateType;
        CocoaPercentage = cocoaPercentage;
        HasFilling = hasFilling;
        FillingType = fillingType;
    }

    public override string ToString()
    {
        var fillingInfo = HasFilling ? $", начинка: {FillingType}" : "";
        return $"{base.ToString()}, Вид: {ChocolateType} шоколад ({CocoaPercentage:F0}% какао){fillingInfo}";
    }
}
