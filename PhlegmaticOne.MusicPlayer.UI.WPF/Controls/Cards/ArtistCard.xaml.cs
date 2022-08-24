using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ArtistCard
{
    public ArtistCard()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var controlWidth = ActualWidth;
        var textBlockWidth = TextBlock.ActualWidth;
        var relative = controlWidth - textBlockWidth - 10;
        var doubleAnimation = new DoubleAnimation()
        {
            From = 10,
            To = relative,
            AutoReverse = true,
            Duration = TimeSpan.FromSeconds(8),
            AccelerationRatio = 0.1,
            DecelerationRatio = 0.1,
            RepeatBehavior = RepeatBehavior.Forever
        };
        
        TextBlock.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
    }
}