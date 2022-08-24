using System;
using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class CollectionListing
{
    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ICommand), typeof(CollectionListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandProperty = DependencyProperty.Register(
        nameof(OnLoadCommand), typeof(ICommand), typeof(CollectionListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandParameterProperty = DependencyProperty.Register(
        nameof(OnLoadCommandParameter), typeof(object), typeof(CollectionListing), new PropertyMetadata(default(object)));

    public object OnLoadCommandParameter
    {
        get => GetValue(OnLoadCommandParameterProperty);
        set => SetValue(OnLoadCommandParameterProperty, value);
    }

    public ICommand ItemClickCommand
    {
        get => (ICommand)GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }
    public ICommand OnLoadCommand
    {
        get => (ICommand)GetValue(OnLoadCommandProperty);
        set => SetValue(OnLoadCommandProperty, value);
    }
    public CollectionListing()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        OnLoadCommand?.Execute(OnLoadCommandParameter);
    }
}