namespace Lab_5.Models;

public class WaffleCandy : Sweet
{
    private int _layersCount;

    public bool HasGlaze { get; }

    public int LayersCount
    {
        get => _layersCount;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Layers count must be greater than zero.");
            }
            _layersCount = value;
        }
    }

    public WaffleCandy(
        string name,
        double weight,
        double sugarPercentage,
        string manufacturer,
        int layersCount,
        bool hasGlaze)
        : base(name, weight, sugarPercentage, manufacturer)
    {
        LayersCount = layersCount;
        HasGlaze = hasGlaze;
    }

    public override string ToString()
    {
        var glazeInfo = HasGlaze ? "в шоколадной глазури" : "без глазури";
        return $"{base.ToString()}, Вафельных слоев: {LayersCount}, Покрытие: {glazeInfo}";
    }
}
