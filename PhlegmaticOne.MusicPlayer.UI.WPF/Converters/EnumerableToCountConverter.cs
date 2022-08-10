using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class EnumerableToCountConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<object> enumerable)
        {
            return enumerable.Count();
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}