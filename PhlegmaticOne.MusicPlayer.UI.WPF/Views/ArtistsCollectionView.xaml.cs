using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Views;

public partial class ArtistsView
{
    public static readonly DependencyProperty OnLoadCommandProperty = DependencyProperty.Register(
        nameof(OnLoadCommand), typeof(ICommand), typeof(ArtistsView), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandParameterProperty = DependencyProperty.Register(
        nameof(OnLoadCommandParameter), typeof(object), typeof(ArtistsView), new PropertyMetadata(default(object)));

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
    public ArtistsView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        OnLoadCommand?.Execute(OnLoadCommandParameter);
    }
}