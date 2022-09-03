using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class CollectionOfEntitiesToStringConverter : IValueConverter
{
    public static string Comma = ", ";
    public static string Slash = "/ ";
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string separator) return string.Empty;

        return value switch
        {
            ICollection<Artist> artists => string.Join(separator, artists.Select(x => x.Name)),
            ICollection<Genre> genres => string.Join(separator, genres.Select(x => x.Name)),
            ICollection<string> names => string.Join(separator, names),
            ICollection<ArtistLinkViewModel> artists => string.Join(separator, artists.Select(x => x.Name)),
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
}