using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class MoveableImageBackground
{
    public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
        nameof(Image), typeof(Bitmap), typeof(MoveableImageBackground), new PropertyMetadata(default(Bitmap)));

    public static readonly DependencyProperty SlideTimeProperty = DependencyProperty.Register(
        nameof(SlideTime), typeof(Duration), typeof(MoveableImageBackground), new PropertyMetadata(default(Duration)));

    public static readonly DependencyProperty BlurEffectProperty = DependencyProperty.Register(
        nameof(BlurEffect), typeof(double), typeof(MoveableImageBackground), new PropertyMetadata(default(double)));

    public static readonly DependencyProperty ImageOpacityProperty = DependencyProperty.Register(
        nameof(ImageOpacity), typeof(double), typeof(MoveableImageBackground), new PropertyMetadata(default(double)));

    public double ImageOpacity
    {
        get => (double) GetValue(ImageOpacityProperty);
        set => SetValue(ImageOpacityProperty, value);
    }
    public double BlurEffect
    {
        get => (double) GetValue(BlurEffectProperty);
        set => SetValue(BlurEffectProperty, value);
    }
    public Duration SlideTime
    {
        get => (Duration) GetValue(SlideTimeProperty);
        set => SetValue(SlideTimeProperty, value);
    }
    public Bitmap Image
    {
        get => (Bitmap) GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public MoveableImageBackground()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var actualHeight = ActualHeight;
        var imageHeight = ImageContent.ActualHeight;
        var relative = actualHeight - imageHeight;
        var animation = new DoubleAnimation()
        {
            From = 0,
            To = relative,
            AutoReverse = true,
            Duration = SlideTime,
            AccelerationRatio = 0.1,
            DecelerationRatio = 0.1,
            RepeatBehavior = RepeatBehavior.Forever
        };

        ImageContent.BeginAnimation(Canvas.TopProperty, animation);
    }
}