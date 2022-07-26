using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class TimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TimeSpan timeSpan) return null;
        var seconds = timeSpan.Seconds >= 10 ? timeSpan.Seconds.ToString() : "0" + timeSpan.Seconds;
        return $"{timeSpan.Minutes}:{seconds}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
}