using Lab_5.Models;

namespace Lab_5.Comparers;

public class SweetWeightComparer : IComparer<Sweet>
{
    public int Compare(Sweet? x, Sweet? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x is null)
        {
            return -1;
        }

        if (y is null)
        {
            return 1;
        }

        return x.Weight.CompareTo(y.Weight);
    }
}
