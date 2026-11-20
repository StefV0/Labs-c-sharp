using Lab_5.Models;

namespace Lab_5.Comparers;

public class SweetSugarComparer : IComparer<Sweet>
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

        return x.SugarPercentage.CompareTo(y.SugarPercentage);
    }
}
