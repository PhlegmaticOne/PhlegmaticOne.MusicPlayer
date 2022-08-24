using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ItemCardsListing
{
    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ICommand), typeof(ItemCardsListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandProperty = DependencyProperty.Register(
        nameof(OnLoadCommand), typeof(ICommand), typeof(ItemCardsListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandParameterProperty = DependencyProperty.Register(
        nameof(OnLoadCommandParameter), typeof(object), typeof(ItemCardsListing), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty CanExecuteOnLoadCommandProperty = DependencyProperty.Register(
        nameof(CanExecuteOnLoadCommand), typeof(bool), typeof(ItemCardsListing), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty CardControlTemplateProperty = DependencyProperty.Register(
        nameof(CardControlTemplate), typeof(ControlTemplate), typeof(ItemCardsListing), new PropertyMetadata(default(ControlTemplate)));

    public static readonly DependencyProperty RowsToShowProperty = DependencyProperty.Register(
        nameof(RowsToShow), typeof(int), typeof(ItemCardsListing), new PropertyMetadata(default(int)));

    public int RowsToShow
    {
        get => (int) GetValue(RowsToShowProperty);
        set => SetValue(RowsToShowProperty, value);
    }
    public ControlTemplate CardControlTemplate
    {
        get => (ControlTemplate) GetValue(CardControlTemplateProperty);
        set => SetValue(CardControlTemplateProperty, value);
    }

    public bool CanExecuteOnLoadCommand
    {
        get => (bool) GetValue(CanExecuteOnLoadCommandProperty);
        set => SetValue(CanExecuteOnLoadCommandProperty, value);
    }
    public object OnLoadCommandParameter
    {
        get => GetValue(OnLoadCommandParameterProperty);
        set => SetValue(OnLoadCommandParameterProperty, value);
    }
    public ICommand? OnLoadCommand
    {
        get => (ICommand?) GetValue(OnLoadCommandProperty);
        set => SetValue(OnLoadCommandProperty, value);
    }

    public ICommand ItemClickCommand
    {
        get => (ICommand) GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }
    public ItemCardsListing()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (CanExecuteOnLoadCommand && OnLoadCommand is not null)
        {
            OnLoadCommand.Execute(OnLoadCommandParameter);
        }
    }

    private void OnListViewItemClicked(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListViewItem listViewItem)
        {
            ItemClickCommand?.Execute(listViewItem.DataContext);
        }
    }
}