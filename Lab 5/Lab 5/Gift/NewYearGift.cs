using System.Collections;
using System.Text;
using Lab_5.Comparers;
using Lab_5.Models;

namespace Lab_5.Gift;

public class NewYearGift : IEnumerable<Sweet>
{
    private readonly List<Sweet> _sweets = [];

    public string Title { get; }
    public IReadOnlyList<Sweet> Sweets => _sweets.AsReadOnly();
    public int Count => _sweets.Count;

    public NewYearGift(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        }

        Title = title;
    }

    public void Add(Sweet sweet)
    {
        ArgumentNullException.ThrowIfNull(sweet);
        _sweets.Add(sweet);
    }

    public void AddRange(IEnumerable<Sweet> sweets)
    {
        ArgumentNullException.ThrowIfNull(sweets);
        foreach (var sweet in sweets)
        {
            Add(sweet);
        }
    }

    public double GetTotalWeight()
    {
        return _sweets.Sum(s => s.Weight);
    }

    public double GetTotalSugarWeight()
    {
        return _sweets.Sum(s => s.TotalSugarWeight);
    }

    public void Sort()
    {
        _sweets.Sort();
    }

    public void Sort(IComparer<Sweet> comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);
        _sweets.Sort(comparer);
    }

    public void SortByWeight()
    {
        _sweets.Sort(new SweetWeightComparer());
    }

    public void SortBySugarPercentage()
    {
        _sweets.Sort(new SweetSugarComparer());
    }

    public IEnumerable<Sweet> FindBySugarRange(double minSugarPercentage, double maxSugarPercentage)
    {
        if (minSugarPercentage > maxSugarPercentage)
        {
            throw new ArgumentException("Minimum sugar percentage cannot be greater than maximum.");
        }

        return _sweets.Where(s => s.SugarPercentage >= minSugarPercentage && s.SugarPercentage <= maxSugarPercentage);
    }

    public IEnumerator<Sweet> GetEnumerator()
    {
        return _sweets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== {Title} ===");
        sb.AppendLine($"Количество сладостей: {Count} шт.");
        sb.AppendLine($"Общий вес подарка: {GetTotalWeight():F1} г");
        sb.AppendLine($"Общий вес сахара: {GetTotalSugarWeight():F1} г");
        sb.AppendLine("Состав:");

        for (var i = 0; i < _sweets.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {_sweets[i]}");
        }

        return sb.ToString();
    }
}
