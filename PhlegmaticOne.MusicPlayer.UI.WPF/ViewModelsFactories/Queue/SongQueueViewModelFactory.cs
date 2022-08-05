using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;

public class SongQueueViewModelFactory : ISongQueueViewModelFactory
{
    private readonly ISongsQueue _songsQueue;
    private readonly IPlayer _player;
    private readonly IValueProvider<Song> _songValueProvider;
    private readonly IValueProvider<Album> _albumValueProvider;

    public SongQueueViewModelFactory(ISongsQueue songsQueue, IPlayer player, IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider)
    {
        _songsQueue = songsQueue;
        _player = player;
        _songValueProvider = songValueProvider;
        _albumValueProvider = albumValueProvider;
    }
    public BaseViewModel CreateQueueViewModel()
    {
        return new SongQueueViewModel(_songsQueue, _player, _songValueProvider, _albumValueProvider);
    }
}