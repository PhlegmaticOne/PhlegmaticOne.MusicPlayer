using System;
using System.Windows;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.KeyHandlers;
using PhlegmaticOne.MusicPlayer.UI.WPF.AttachedProperties;
using PhlegmaticOne.MusicPlayer.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF;

public partial class MainWindow
{
    private readonly MainViewModel _mainViewModel;
    private readonly IGlobalKeyHandler _globalKeyHandler;

    public MainWindow(MainViewModel mainViewModel, IGlobalKeyHandler globalKeyHandler)
    {
        InitializeComponent();
        DataContext = mainViewModel;
        _mainViewModel = mainViewModel;
        _globalKeyHandler = globalKeyHandler;
        Loaded += OnLoaded;
        SourceInitialized += OnSourceInitialized;
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        _globalKeyHandler.HanleKey(e.Key.ToString());
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