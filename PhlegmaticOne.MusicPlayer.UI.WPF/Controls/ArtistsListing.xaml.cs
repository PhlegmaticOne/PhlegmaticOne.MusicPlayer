using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ArtistsListing
{
    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ICommand), typeof(ArtistsListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandProperty = DependencyProperty.Register(
        nameof(OnLoadCommand), typeof(ICommand), typeof(ArtistsListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandParameterProperty = DependencyProperty.Register(
        nameof(OnLoadCommandParameter), typeof(object), typeof(ArtistsListing), new PropertyMetadata(default(object)));

    public object OnLoadCommandParameter
    {
        get => GetValue(OnLoadCommandParameterProperty);
        set => SetValue(OnLoadCommandParameterProperty, value);
    }
    public ICommand OnLoadCommand
    {
        get => (ICommand) GetValue(OnLoadCommandProperty);
        set => SetValue(OnLoadCommandProperty, value);
    }
    public ICommand ItemClickCommand
    {
        get => (ICommand) GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }
    public ArtistsListing()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        OnLoadCommand?.Execute(OnLoadCommandParameter);
    }
}