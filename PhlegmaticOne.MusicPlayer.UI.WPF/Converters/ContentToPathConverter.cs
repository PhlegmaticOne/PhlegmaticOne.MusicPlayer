using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class ContentToPathConverter : MarkupExtension, IValueConverter
{
    private static readonly string _assemblyName;
    private readonly Dictionary<string, string> _contentToPath = new()
    {
        {"home", "home"},
        {"addnew", "new"},
        {"tracks", "tracks"},
        {"artists", "artist"},
        {"albums", "album"},
        {"playlists", "playlist"},
        {"downloaded", "tracks"},
        {"settings", "setting"},
    };

    static ContentToPathConverter()
    {
        _assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string path) return "";

        var image = new System.Windows.Controls.Image
        {
            Source = new BitmapImage(
                new Uri($@"/{_assemblyName};component/Images/{_contentToPath[path.ToLower()]}.png", UriKind.Relative))
        };
        return image.Source;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}