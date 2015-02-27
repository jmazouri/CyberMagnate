using System;

public static class MiscExtensions
{
    public static bool CanConvertTo<T>(this object input)
    {
        object result = null;

        try
        {
            result = Convert.ChangeType(input, typeof(T));
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static int ZeroMin(this int input)
    {
        if (input < 0)
        {
            return 0;
        }

        return input;
    }
}
