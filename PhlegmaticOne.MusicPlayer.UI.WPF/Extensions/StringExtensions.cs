using System;
using System.Linq;
using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Extensions;

public static class StringExtensions
{
    public static GridLength ToGridLength(this string value)
    {
        if (double.TryParse(value, out var widthInPixels))
        {
            return new GridLength(widthInPixels);
        }

        return value == "*" ?
            new GridLength(1, GridUnitType.Star) :
            new GridLength(double.Parse(value.Split('*', StringSplitOptions.RemoveEmptyEntries).First()), GridUnitType.Star);
    }
}