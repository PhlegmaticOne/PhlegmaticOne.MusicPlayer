using System.Collections.ObjectModel;
using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class HomeViewModel : PlayerTrackableViewModel
{
    public ObservableCollection<TrackBaseViewModel> Tracks { get; set; }
    public HomeViewModel(IPlayerService playerService, ILikeService likeService) : base(playerService, likeService)
    {
        Tracks = new();
        Play = DelegateCommandFactory.CreateCommand(PlayAction, _ => true);
        PlayPause = DelegateCommandFactory.CreateCommand(PlayPauseAct, _ => true);
        AddTracks();
    }
    public IDelegateCommand Play { get; set; }
    public IDelegateCommand PlayPause { get; set; }

    private void PlayAction(object? parameter)
    {
        if (parameter is TrackBaseViewModel trackBaseViewModel)
        {
            CurrentSong = trackBaseViewModel;
            IsStopped = false;
            IsPaused = false;
        }
    }
    private void PlayPauseAct(object? parameter)
    {
        if (parameter is TrackBaseViewModel trackBaseViewModel)
        {
            IsPaused = true;
        }
    }
    private void AddTracks()
    {
        var albumCover = new AlbumCover()
        {
            Cover = (Bitmap) Image.FromFile(@"D:\Media\Music\Paysage D'Hiver\[1999] Kerker\front.jpg")
        };

        var collectionLink = new CollectionLinkViewModel()
        {
            Cover = albumCover,
            Title = "Kerker"
        };

        var artistLinks = new List<ArtistLinkViewModel>()
        {
            new()
            {
                Name = "Paysage d'hiver"
            }
        };

        Tracks.Add(new TrackBaseViewModel()
        {
            ArtistLinks = artistLinks,
            CollectionLink = collectionLink,
            Title = "Tiefe",
            LocalUrl = @"D:\Media\Music\Paysage D'Hiver\[1999] Kerker\1. Tiefe.flac",
            Duration = new TimeSpan(0, 10, 00),
            IsDownloaded = true,
            IsFavorite = false,
        });

        Tracks.Add(new TrackBaseViewModel()
        {
            ArtistLinks = artistLinks,
            CollectionLink = collectionLink,
            Title = "Schritte",
            LocalUrl = @"D:\Media\Music\Paysage D'Hiver\[1999] Kerker\2. Schritte.flac",
            Duration = new TimeSpan(0, 10, 00),
            IsDownloaded = true,
            IsFavorite = true,
        });

        Tracks.Add(new TrackBaseViewModel()
        {
            ArtistLinks = artistLinks,
            CollectionLink = collectionLink,
            LocalUrl = @"D:\Media\Music\Paysage D'Hiver\[1999] Kerker\3. Schatten.flac",
            Title = "Schatten",
            Duration = new TimeSpan(0, 10, 00),
            IsDownloaded = true,
            IsFavorite = true,
        });

        Tracks.Add(new TrackBaseViewModel()
        {
            ArtistLinks = artistLinks,
            CollectionLink = collectionLink,
            LocalUrl = @"D:\Media\Music\Paysage D'Hiver\[1999] Kerker\4. Gang.flac",
            Title = "Gang",
            Duration = new TimeSpan(0, 10, 00),
            IsDownloaded = true,
            IsFavorite = true,
        });
    }
}