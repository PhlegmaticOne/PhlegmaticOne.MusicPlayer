using System.Windows;
using System.Windows.Media.Animation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

public static class AnimationsGenerator
{
    public static DoubleAnimation? GenerateSlideAnimation(FrameworkElement contentElement, 
        FrameworkElement containerElement,
        Duration slideTime)
    {
        var contentWidth = contentElement.ActualWidth;
        var containerWidth = containerElement.ActualWidth;

        if (containerWidth >= contentWidth)
        {
            return null;
        }

        var relative = containerWidth - contentWidth - 10;
        var animation = CreateDoubleAnimation(relative, slideTime);

        return animation;
    }

    private static DoubleAnimation CreateDoubleAnimation(double to, Duration slideTime) =>
        new()
        {
            From = 10,
            To = to,
            AutoReverse = true,
            Duration = slideTime,
            AccelerationRatio = 0.1,
            DecelerationRatio = 0.1,
            RepeatBehavior = RepeatBehavior.Forever
        };
}