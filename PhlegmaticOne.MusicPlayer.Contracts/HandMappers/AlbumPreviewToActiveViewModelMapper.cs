using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.HandMappers;

public class AlbumPreviewToActiveViewModelMapper : HandMapperBase<AlbumPreviewViewModel, ActiveAlbumViewModel>
{
    public override ActiveAlbumViewModel? Map(AlbumPreviewViewModel from)
    {
        var songs = Parameters["CollectionSongs"] as List<Song>;
        return new ActiveAlbumViewModel()
        {
            Artists = from.Artists,
            Title = from.Title,
            Cover = from.Cover,
            IsFavorite = from.IsFavorite,
            IsDownloaded = songs.All(s => string.IsNullOrEmpty(s.LocalUrl) == false),
            IsDownloading = false,
            Id = from.Id,
            AlbumType = from.AlbumType,
            DateAdded = from.DateAdded,
            YearReleased = from.YearReleased,
            Tracks = songs!.Select(x => new TrackBaseViewModel
            {
                ArtistLinks = x.Artists.Select(a => new ArtistLinkViewModel
                {
                    Id = a.Id,
                    IsFavorite = a.IsFavorite,
                    Name = a.Name
                }).ToList(),
                CollectionLink = new CollectionLinkViewModel()
                {
                    Cover = from.Cover,
                    Id = from.Id,
                    Title = from.Title,
                },
                IsDownloaded = string.IsNullOrEmpty(x.LocalUrl) == false,
                IsFavorite = x.IsFavorite,
                IsDownloading = false,
                LocalUrl = x.LocalUrl,
                Title = x.Title,
                Duration = x.Duration,
                Id = x.Id,
                OnlineUrl = x.OnlineUrl,
                TimePlayed = x.TimePlayed
            }).ToList()
        };
    }
}