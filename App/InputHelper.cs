using System.Globalization;

public static class InputHelper
{
    public static decimal ParseDecimal(string number)
    {
        if (number == null)
        {
            throw new ArgumentNullException();
        }

        if (number.Contains('.') && number.Contains(','))
        {
            throw new FormatException();
        }

        NumberStyles style = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
        decimal res;

        if (decimal.TryParse(number, style, CultureInfo.InvariantCulture, out res))
        {
            return res;
        }

        if (decimal.TryParse(number, style, CultureInfo.GetCultureInfo("uk-UA"), out res))
        {
            return res;
        }

        throw new FormatException();
    }
}