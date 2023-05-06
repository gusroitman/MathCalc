using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using static MudBlazor.Colors;

internal static class IndexHelpers
{

    public static T Max<T>(T[] values) where T : IComparable<T>
    {
        T _max;

        _max = values[0];

        foreach (T _cur in values)
        {
            if (_cur.CompareTo(_max) > 0)
                _max = _cur;
        }
        return _max;
    }
    public static T Min<T>(T[] values) where T : IComparable<T>
    {
        T _min;

        _min = values[0];

        foreach (T _cur in values)
        {
            if (_cur.CompareTo(_min) < 0)
                _min = _cur;
        }
        return _min;
    }
    public static T Plus<T>(T first, T second) where T : INumber<T>
    {
        T result = T.Zero;

        result = first + second;

        return result;
    }
    public static T Minus<T>(T first, T second) where T : INumber<T>
    {
        T result = T.Zero;

        result = first - second;

        return result;
    }
    public static T Times<T>(T first, T second) where T : INumber<T>
    {
        T result = T.Zero;

        result = first * second;

        return result;
    }
    public static T Divide<T>(T first, T second) where T : INumber<T>
    {
        T result = T.Zero;

        result = first / second;

        return result;
    }
    public static T AddAll<T>(T[] values) where T : INumber<T>
    {
        T result = T.Zero;

        foreach (var value in values)
        {
            result += value;
        }
        return result;
    }
    public static double AvgAll<T>(T[] values) where T : INumber<T>
    {
        var valuesAvg = values.ToArray();
        double avg = Queryable.Average((IQueryable<double>)valuesAvg.AsQueryable());

        return Math.Round(avg, 2, MidpointRounding.AwayFromZero);
    }
    public static T? Rand<T>(T[] values)
    {
        if (values.Length == 0)
            return default;
        if (values.Length == 1)
            return values[0];

        Random rnd = new Random(values.Length);

        return values[(rnd.Next(0, values.Length))];
    }
}