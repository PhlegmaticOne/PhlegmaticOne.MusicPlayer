using System;
using System.Windows;
using PhlegmaticOne.MusicPlayer.UI.WPF.AttachedProperties;
using PhlegmaticOne.MusicPlayer.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF;

public partial class MainWindow
{
    private readonly MainViewModel _mainViewModel;
    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = mainViewModel;
        _mainViewModel = mainViewModel;
        Loaded += OnLoaded;
        SourceInitialized += OnSourceInitialized;
    }

    private void OnSourceInitialized(object? sender, EventArgs e) => WindowSizing.WindowInitialized(this);

    private void OnLoaded(object sender, RoutedEventArgs e) => WindowState = WindowState.Maximized;

    private void MinimizeButton_OnClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        _mainViewModel.Close();
        Application.Current.Shutdown();
    }
}