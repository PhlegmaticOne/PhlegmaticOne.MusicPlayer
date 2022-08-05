using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class SongQueueViewModel : PlayerTrackableViewModel
{
    public ObservableCollection<Song> Songs { get; }
    public SongQueueViewModel(ISongsQueue songsQueue, IPlayer player, IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider) : 
        base(player, songsQueue, songValueProvider, albumValueProvider)
    {
        Songs = new();
        SongsQueue.QueueChanged += SongsQueueOnQueueChanged;
        if (Songs.Any() == false)
        {
            AddSongs(songsQueue.Songs);
        }

        CurrentAlbum = AlbumValueProvider.Get();
        TrySetSong();
    }

    private void SongsQueueOnQueueChanged(object? sender, CollectionChangedEventArgs<Song> e)
    {
        switch (e.CollectionChangedType)
        {
            case CollectionChangedType.Added:
            {
                AddSongs(e.Entities);
                break;
            }
            case CollectionChangedType.Removed:
            {
                RemoveSongs(e.Entities);
                break;
            }
        }
    }

    private void AddSongs(IEnumerable<Song> songs)
    {
        foreach (var entity in songs)
        {
            Songs.Add(entity);
        }
    }

    private void RemoveSongs(IEnumerable<Song> songs)
    {
        foreach (var entity in songs)
        {
            Songs.Remove(entity);
        }
    }
}