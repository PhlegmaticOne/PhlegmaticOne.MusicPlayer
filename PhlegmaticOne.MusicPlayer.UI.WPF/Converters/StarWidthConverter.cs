using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class StarWidthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var listview = value as ListView;
        var width = listview.ActualWidth;
        var gridView = listview.View as GridView;
        foreach (var gvColumn in gridView.Columns)
        {
            if (double.IsNaN(gvColumn.Width) == false)
            {
                width -= gvColumn.Width;
            }
        }
        return width - listview.Padding.Right - listview.Padding.Left;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}