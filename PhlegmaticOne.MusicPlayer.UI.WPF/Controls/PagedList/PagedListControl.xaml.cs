using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class PagedListControl
{
    public static readonly DependencyProperty ItemsViewProperty = DependencyProperty.Register(
        nameof(ItemsView), typeof(FrameworkElement), typeof(PagedListControl), new PropertyMetadata(default(FrameworkElement)));

    public FrameworkElement ItemsView
    {
        get => (FrameworkElement) GetValue(ItemsViewProperty);
        set => SetValue(ItemsViewProperty, value);
    }

    public static readonly DependencyProperty OnLoadCommandProperty = DependencyProperty.Register(
        nameof(OnLoadCommand), typeof(ICommand), typeof(PagedListControl), new PropertyMetadata(default(ICommand)));

    public ICommand OnLoadCommand
    {
        get => (ICommand) GetValue(OnLoadCommandProperty);
        set => SetValue(OnLoadCommandProperty, value);
    }
    public PagedListControl()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        OnLoadCommand?.Execute(new object());
    }
}