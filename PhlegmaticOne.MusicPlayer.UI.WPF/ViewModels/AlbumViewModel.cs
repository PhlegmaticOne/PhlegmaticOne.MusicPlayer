using System.Collections.Generic;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : PlayerTrackableViewModel
{
    private bool _isFirstSongWillPlay;
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(Album album, ISongsQueue songsQueue, IPlayer player, IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider) :
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