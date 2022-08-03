using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;

public class AlbumFeaturesProvider : IAlbumFeaturesProvider
{
    private readonly INavigator _navigator;

    public AlbumFeaturesProvider(INavigator navigator)
    {
        _navigator = navigator;
        AlbumFeatures = new Dictionary<string, ICommand>()
        {
            { "Go to artist", new DelegateCommand(GoToArtist, _ => true) }
        };
    }
    public IDictionary<string, ICommand> AlbumFeatures { get; }

    private void GoToArtist(object? parameter)
    {
        if(parameter is not Entities.Artist artist) return;
    }
}