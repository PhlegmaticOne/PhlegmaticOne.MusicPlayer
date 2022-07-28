using System;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public class MusicViewModelFactory : IMusicViewModelsFactory
{
    private readonly IAlbumViewModelFactory _albumViewModelFactory;

    public MusicViewModelFactory(IAlbumViewModelFactory albumViewModelFactory)
    {
        _albumViewModelFactory = albumViewModelFactory;
    }
    public BaseViewModel CreateAlbumViewModel(Album album) => _albumViewModelFactory.CreateViewModel(album);

    public BaseViewModel CreatePlaylistViewModel(Playlist playlist)
    {
        throw new NotImplementedException();
    }

    public BaseViewModel CreateArtistViewModel(Artist artist)
    {
        throw new NotImplementedException();
    }
}