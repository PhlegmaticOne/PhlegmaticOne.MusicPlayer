﻿using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation.ViewModelFactories;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;

public class MusicNavigation<TFrom, TTo>
    where TFrom : EntityBaseViewModel
    where TTo : ApplicationBaseViewModel
{
    private readonly INavigationService _navigator;
    private readonly IMusicViewModelsFactory<TFrom, TTo> _viewModelsFactory;

    public MusicNavigation(INavigationService navigator, IMusicViewModelsFactory<TFrom, TTo> viewModelsFactory)
    {
        _navigator = navigator;
        _viewModelsFactory = viewModelsFactory;
        NavigateToMusicCommand = new DelegateCommand(Navigate, _ => true);
    }
    public ICommand NavigateToMusicCommand { get; set; }
    private async void Navigate(object? parameter)
    {
        if (parameter is TFrom from)
        {
            var viewModel = await _viewModelsFactory.CreateViewModelAsync(from);
            _navigator.NavigateTo(viewModel);
        }
    }
}