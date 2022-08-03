using System.Collections.Generic;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;

public interface IAlbumFeaturesProvider
{
    public IDictionary<string, ICommand> AlbumFeatures { get; }
}