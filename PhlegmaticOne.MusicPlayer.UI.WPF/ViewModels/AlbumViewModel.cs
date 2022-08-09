using System.Collections.Generic;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : PlayerTrackableViewModel
{
    private bool _isFirstSongWillPlay;
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(AlbumEntityViewModel album, ISongsQueue songsQueue, IPlayer player,
        IValueProvider<SongEntityViewModel> songValueProvider, IValueProvider<AlbumEntityViewModel> albumValueProvider) :
        base(player, songsQueue, songValueProvider, albumValueProvider)
    {
        _isFirstSongWillPlay = true;
        AlbumValueProvider.Set(album);
        TrySetSong();
    }
    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirstSongWillPlay)
        {
            SongsQueue.Clear();
            SongsQueue.Enqueue(CurrentAlbum.Songs);
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }
}