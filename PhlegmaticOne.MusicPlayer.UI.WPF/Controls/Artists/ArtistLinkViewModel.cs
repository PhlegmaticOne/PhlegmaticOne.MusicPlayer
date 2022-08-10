using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Artists;

public class ArtistLinkViewModel : BaseViewModel
{
    private readonly Guid _artistGuid;
    private readonly INavigator _navigator;
    private readonly IUnitOfWork _unitOfWork;
    public string ArtistName { get; set; }

    public ArtistLinkViewModel(string artistName, Guid artistGuid, INavigator navigator, IUnitOfWork unitOfWork)
    {
        _artistGuid = artistGuid;
        _navigator = navigator;
        _unitOfWork = unitOfWork;
        ArtistName = artistName;

        NavigateToArtistCommand = new(NavigateToArtist, _ => true);
    }
    public DelegateCommand NavigateToArtistCommand { get; set; }

    private async void NavigateToArtist(object? parameter)
    {
        var artistRepository = _unitOfWork.GetRepository<Artist>();
        var artist = await artistRepository.GetFirstOrDefaultAsync(predicate: a => a.Id == _artistGuid);
        var viewModel = new BaseViewModel(); //artist view model factory
        _navigator.NavigateTo(viewModel);
    }
}