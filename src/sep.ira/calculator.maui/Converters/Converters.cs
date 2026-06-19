using System.Globalization;

namespace cc.isr.Finance.Sep.Ira.Converters;

/// <summary>
/// Converter that inverts a boolean value.
/// </summary>
public class InvertedBoolConverter : IValueConverter
{
    public object Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if ( value is bool boolValue )
        {
            return !boolValue;
        }
        return true;
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if ( value is bool boolValue )
        {
            return !boolValue;
        }
        return false;
    }
}

/// <summary>
/// Converter that converts string to boolean (true if string is not null or empty).
/// </summary>
public class StringNotNullOrEmptyBoolConverter : IValueConverter
{
    public object Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if ( value is string stringValue )
        {
            return !string.IsNullOrWhiteSpace( stringValue );
        }
        return false;
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotImplementedException();
    }
}
