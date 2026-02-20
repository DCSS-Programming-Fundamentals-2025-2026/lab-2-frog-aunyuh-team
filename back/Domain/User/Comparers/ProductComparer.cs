using System.Collections;

namespace lab_1_frog_aunyuh_team.back.Domain.User.Comparers;

public class ProductComparer : IComparer
{
    public int Compare(object? x, object? y)
    {
        if (!(x is Product && y is Product))
        {
            throw new ArgumentException();
        }

        return ((Product)x).CompareTo((Product)y);
    }
}