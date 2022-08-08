using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Converters;

public class VolumeToIconConverter : IValueConverter
{
    static VolumeToIconConverter()
    {
        var mutedIcon =
            Geometry.Parse(
                "M3,9H7L12,4V20L7,15H3V9M16.59,12L14,9.41L15.41,8L18,10.59L20.59,8L22,9.41L19.41,12L22,14.59L20.59,16L18,13.41L15.41,16L14,14.59L16.59,12Z");
        var lowVolumeIcon = Geometry.Parse("M7,9V15H11L16,20V4L11,9H7Z");
        var mediumVolumeIcon =
            Geometry.Parse(
                "M5,9V15H9L14,20V4L9,9M18.5,12C18.5,10.23 17.5,8.71 16,7.97V16C17.5,15.29 18.5,13.76 18.5,12Z");
        var highVolumeIcon = Geometry.Parse(
            "M14,3.23V5.29C16.89,6.15 19,8.83 19,12C19,15.17 16.89,17.84 14,18.7V20.77C18,19.86 21,16.28 21,12C21,7.72 18,4.14 14,3.23M16.5,12C16.5,10.23 15.5,8.71 14,7.97V16C15.5,15.29 16.5,13.76 16.5,12M3,9V15H7L12,20V4L7,9H3Z");
        _soundImages = new()
        {
            {0, mutedIcon},
            {0.33, lowVolumeIcon},
            {0.66, mediumVolumeIcon},
            {1, highVolumeIcon}
        };
    }

    private static readonly Dictionary<double, Geometry> _soundImages;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not double volume)
        {
            return null;
        }

        return _soundImages.First(x => x.Key >= volume).Value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}