using System.Windows;
using System.Windows.Media;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Artists;

public partial class ArtistLink
{
    public static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register(
        "HoverColor", typeof(Brush), typeof(ArtistLink),
        new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

    public Brush HoverColor
    {
        get => (Brush)GetValue(HoverColorProperty);
        set => SetValue(HoverColorProperty, value);
    }
    public ArtistLink()
    {
        InitializeComponent();
    }
}