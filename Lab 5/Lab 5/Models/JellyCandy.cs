namespace Lab_5.Models;

public class JellyCandy : Sweet
{
    public string GelatinBase { get; }
    public bool IsSour { get; }

    public JellyCandy(
        string name,
        double weight,
        double sugarPercentage,
        string manufacturer,
        string gelatinBase,
        bool isSour)
        : base(name, weight, sugarPercentage, manufacturer)
    {
        if (string.IsNullOrWhiteSpace(gelatinBase))
        {
            throw new ArgumentException("Gelatin base cannot be null or empty.", nameof(gelatinBase));
        }

        GelatinBase = gelatinBase;
        IsSour = isSour;
    }

    public override string ToString()
    {
        var sourInfo = IsSour ? "кислая обсыпка" : "классический сладкий";
        return $"{base.ToString()}, Желирующий агент: {GelatinBase}, Тип: {sourInfo}";
    }
}
