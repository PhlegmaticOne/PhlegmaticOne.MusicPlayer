using System.Drawing;
using System.Windows;
using Color = System.Windows.Media.Color;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ImageControl
{
    public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
        nameof(Image), typeof(Bitmap), typeof(ImageControl), new PropertyMetadata(default(Bitmap)));

    public static readonly DependencyProperty ShadowEffectProperty = DependencyProperty.Register(
        nameof(ShadowEffect), typeof(double), typeof(ImageControl), new PropertyMetadata(default(double)));

    public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register(
        nameof(BlurRadius), typeof(double), typeof(ImageControl), new PropertyMetadata(default(double)));

    public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(
        nameof(ShadowColor), typeof(Color), typeof(ImageControl), new PropertyMetadata(default(Color)));

    public Color ShadowColor
    {
        get => (Color) GetValue(ShadowColorProperty);
        set => SetValue(ShadowColorProperty, value);
    }
    public double BlurRadius
    {
        get => (double) GetValue(BlurRadiusProperty);
        set => SetValue(BlurRadiusProperty, value);
    }
    public double ShadowEffect
    {
        get => (double) GetValue(ShadowEffectProperty);
        set => SetValue(ShadowEffectProperty, value);
    }
    public Bitmap Image
    {
        get => (Bitmap) GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
    public ImageControl()
    {
        InitializeComponent();
    }
}