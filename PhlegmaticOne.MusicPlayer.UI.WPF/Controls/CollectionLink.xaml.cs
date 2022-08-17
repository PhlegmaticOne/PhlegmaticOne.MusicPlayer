using System.Windows;
using System.Windows.Media;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class CollectionLink
{
    public static readonly DependencyProperty ForegroundHoverProperty = DependencyProperty.Register(
        nameof(ForegroundHover), typeof(Brush), typeof(CollectionLink), new PropertyMetadata(default(Brush)));

    public Brush ForegroundHover
    {
        get => (Brush) GetValue(ForegroundHoverProperty);
        set => SetValue(ForegroundHoverProperty, value);
    }

    public CollectionLink()
    {
        InitializeComponent();
    }
}