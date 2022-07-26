using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class EntityToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
        value?.ToString() ?? string.Empty;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
        DependencyProperty.UnsetValue;
}