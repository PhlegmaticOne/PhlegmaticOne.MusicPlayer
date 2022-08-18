﻿using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ReloadButton
{
    public static readonly DependencyProperty ViewModelToReloadProperty = DependencyProperty.Register(
        nameof(ViewModelToReload), typeof(BaseViewModel), typeof(ReloadButton), new PropertyMetadata(default(BaseViewModel)));

    public BaseViewModel ViewModelToReload
    {
        get => (BaseViewModel)GetValue(ViewModelToReloadProperty);
        set => SetValue(ViewModelToReloadProperty, value);
    }
    public ReloadButton()
    {
        InitializeComponent();
    }
}