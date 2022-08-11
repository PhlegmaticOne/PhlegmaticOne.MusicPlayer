using Mapster;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperRegistration;

public class Registration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CollectionBase, CollectionLinkViewModel>();

        config.NewConfig<Artist, ArtistLinkViewModel>()
            .Inherits<EntityBase, ArtistBaseViewModel>();

        config.NewConfig<Song, TrackBaseViewModel>()
            .Inherits<EntityBase, TrackBaseViewModel>()
            .Ignore(x => x.IsDownloaded)
            .Ignore(x => x.IsDownloading)
            .Map(x => x.OnlineUrl, o => o.OnlineUrl)
            .Map(x => x.LocalUrl, o => o.LocalUrl);

        config.NewConfig<Album, AlbumPreviewViewModel>()
            .Inherits<EntityBase, CollectionBaseViewModel>()
            .Ignore(x => x.Cover)
            .Ignore(x => x.IsDownloaded)
            .Ignore(x => x.IsDownloading)
            .Map(x => x.Artists, o => o.Artists.Select(x => x.Name))
            .AfterMapping((album, viewModel) => viewModel.Cover = album.AlbumCover);

        config.NewConfig<Album, ActiveAlbumViewModel>()
            .Inherits<EntityBase, CollectionBaseViewModel>()
            .Ignore(x => x.Cover)
            .Ignore(x => x.IsDownloaded)
            .Ignore(x => x.IsDownloading)
            .Map(x => x.Artists, o => o.Artists)
            .Map(x => x.Tracks, x => x.Songs);


        //config.NewConfig<Album, ActiveAlbumViewModel>()
        //    .Ignore(x => x.Cover)
        //    .Ignore(x => x.IsDownloaded)
        //    .Ignore(x => x.IsDownloaded)
        //    .Map(x => x.Tracks, o => o.Songs);

        //config.NewConfig<Playlist, PlaylistPreviewViewModel>()
        //    .Ignore(x => x.Cover)
        //    .Map(x => x.TracksCount, o => o.Songs.Count);

        //config.NewConfig<Playlist, ActivePlaylistViewModel>()
        //    .Ignore(x => x.Cover)
        //    .Ignore(x => x.IsDownloaded)
        //    .Ignore(x => x.IsDownloading)
        //    .Map(x => x.Tracks, o => o.Songs);


        //config.NewConfig<Artist, ArtistPreviewViewModel>()
        //    .Ignore(x => x.Cover)
        //    .Map(x => x.Genres, o => o.Albums.SelectMany(x => x.Genres).Distinct().Select(x => x.Name));

        //config.NewConfig<Artist, ActiveArtistViewModel>()
        //    .Ignore(x => x.Cover)
        //    .Map(x => x.Albums, o => o.Albums)
        //    .Map(x => x.Tracks, o => o.Albums.SelectMany(x => x.Songs));
    }
}