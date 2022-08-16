using PhlegmaticOne.HandMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.HandMappers;

public class CollectionsToTrackViewModelMapper : HandMapperBase<IEnumerable<CollectionBase>, IEnumerable<TrackBaseViewModel>>
{
    public override IEnumerable<TrackBaseViewModel>? Map(IEnumerable<CollectionBase> from)
    {
        foreach (var collection in from)
        {
            var collectionLink = CreateCollectionLink(collection);
            var artistLinks = CreateArtistLinks(collection);
            foreach (var albumSong in collection.Songs)
            {
                yield return new()
                {
                    Title = albumSong.Title,
                    ArtistLinks = artistLinks,
                    CollectionLink = collectionLink,
                    Duration = albumSong.Duration,
                    Id = albumSong.Id,
                    IsDownloaded = string.IsNullOrEmpty(albumSong.LocalUrl) == false,
                    IsDownloading = false,
                    OnlineUrl = albumSong.OnlineUrl,
                    LocalUrl = albumSong.LocalUrl,
                    IsFavorite = albumSong.IsFavorite,
                    TimePlayed = albumSong.TimePlayed,
                };
            }
        }
    }

    private static CollectionLinkViewModel CreateCollectionLink(CollectionBase album) =>
        new ()
        {
            Id = album.Id,
            Cover = album.AlbumCover,
            Title = album.Title
        };

    private static List<ArtistLinkViewModel> CreateArtistLinks(CollectionBase collection)
    {
        if (collection is Album album)
        {
            return album.Artists.Select(albumArtist => new ArtistLinkViewModel()
            {
                Name = albumArtist.Name,
                Id = albumArtist.Id
            }).ToList();
        }
        return new ()
        {
            new()
            {
                Name = string.Empty,
                Id = Guid.Empty
            }
        };
    }
}