using System;
using System.Globalization;
using System.Windows.Data;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class TimeSpanToLongConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TimeSpan time) return null;

        return time.Ticks;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}