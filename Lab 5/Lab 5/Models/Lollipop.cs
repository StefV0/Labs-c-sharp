namespace Lab_5.Models;

public class Lollipop : Sweet
{
    public string Flavor { get; }
    public bool OnStick { get; }

    public Lollipop(
        string name,
        double weight,
        double sugarPercentage,
        string manufacturer,
        string flavor,
        bool onStick)
        : base(name, weight, sugarPercentage, manufacturer)
    {
        if (string.IsNullOrWhiteSpace(flavor))
        {
            throw new ArgumentException("Flavor cannot be null or empty.", nameof(flavor));
        }

        Flavor = flavor;
        OnStick = onStick;
    }

    public override string ToString()
    {
        var stickInfo = OnStick ? "на палочке" : "леденец-монпансье";
        return $"{base.ToString()}, Вкус: {Flavor}, Форм-фактор: {stickInfo}";
    }
}
