using PhlegmaticOne.HandMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.HandMappers;

public class AlbumToActiveViewModelMapper : HandMapperBase<Album, ActiveAlbumViewModel>
{
    public override ActiveAlbumViewModel? Map(Album from)
    {
        var artistLinks = from.Artists.Select(x => new ArtistLinkViewModel()
        {
            Name = x.Name,
            Id = x.Id
        }).ToList();
        return new ActiveAlbumViewModel()
        {
            Artists = artistLinks,
            Title = from.Title,
            Cover = from.AlbumCover,
            Id = from.Id,
            IsDownloaded = false,
            IsDownloading = false,
            YearReleased = from.YearReleased,
            IsFavorite = from.IsFavorite,
            AlbumType = from.AlbumType,
            DateAdded = from.DateAdded,
            Tracks = from.Songs.Select(x => new TrackBaseViewModel()
            {
                Title = x.Title,
                IsFavorite = x.IsFavorite,
                ArtistLinks = artistLinks,
                CollectionLink = new CollectionLinkViewModel()
                {
                    Title = from.Title,
                    Cover = from.AlbumCover,
                    Id = from.Id
                },
                IsDownloaded = false,
                Id = x.Id,
                Duration = x.Duration,
                LocalUrl = x.LocalUrl,
                OnlineUrl = x.OnlineUrl,
                IsDownloading = false,
                TimePlayed = x.TimePlayed
            }).ToList()
        };
    }
}