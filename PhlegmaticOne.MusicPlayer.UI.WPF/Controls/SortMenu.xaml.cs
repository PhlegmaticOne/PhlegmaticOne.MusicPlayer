﻿using System.Windows;
using System.Windows.Input;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class SortMenu
{
    public static readonly DependencyProperty ViewModelToSortProperty = DependencyProperty.Register(
        nameof(ViewModelToSort), typeof(BaseViewModel), typeof(SortMenu), new PropertyMetadata(default(BaseViewModel)));

    public BaseViewModel ViewModelToSort
    {
        get => (BaseViewModel)GetValue(ViewModelToSortProperty);
        set => SetValue(ViewModelToSortProperty, value);
    }

    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ICommand), typeof(SortMenu), new PropertyMetadata(default(ICommand)));

    public ICommand ItemClickCommand
    {
        get => (ICommand) GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }
    public static readonly DependencyProperty ControlLoadCommandProperty = DependencyProperty.Register(
        nameof(ControlLoadCommand), typeof(ICommand), typeof(SortMenu), new PropertyMetadata(default(ICommand)));

    public ICommand ControlLoadCommand
    {
        get => (ICommand)GetValue(ControlLoadCommandProperty);
        set => SetValue(ControlLoadCommandProperty, value);
    }
    public SortMenu()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ControlLoadCommand?.Execute(null);
    }
}