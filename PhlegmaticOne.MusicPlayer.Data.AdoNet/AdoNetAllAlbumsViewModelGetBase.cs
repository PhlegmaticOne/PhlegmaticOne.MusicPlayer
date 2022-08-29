using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllAlbumsViewModelGetBase : AdoNetViewModelGetBase<AllAlbumsPreviewViewModel>
{
    public AdoNetAllAlbumsViewModelGetBase(ISqlClient sqlClient) : base(sqlClient) { }
    protected override string CommandName => "Get_All_Albums_Preview";

    protected override async Task<AllAlbumsPreviewViewModel> Create(SqlDataReader reader)
    {
        var allAlbums = new List<AlbumPreviewViewModel>();

        while (await reader.ReadAsync())
        {
            var id = await reader.GetFieldValueAsync<Guid>(0);
            var yearReleased = await reader.GetFieldValueAsync<int>(1);
            var albumType = Enum.Parse<AlbumType>(await reader.GetFieldValueAsync<string>(2));
            var onlineUrl = await reader.GetFieldValueAsync<string>(3);
            var albumName = await reader.GetFieldValueAsync<string>(4);
            var dateAdded = await reader.GetFieldValueAsync<DateTime>(5);
            var isFavorite = await reader.GetFieldValueAsync<bool>(6);
            await reader.NextResultAsync();

            await reader.ReadAsync();
            var imageData = await reader.GetFieldValueAsync<byte[]>(0);
            var cover = new AlbumCover()
            {
                Cover = imageData.ToBitmap()
            };
            await reader.NextResultAsync();


            var artistNames = new List<ArtistLinkViewModel>();
            while (await reader.ReadAsync())
            {
                var artistId = await reader.GetFieldValueAsync<Guid>(0);
                var artistName = await reader.GetFieldValueAsync<string>(1);
                artistNames.Add(new ArtistLinkViewModel
                {
                    Id = artistId,
                    Name = artistName
                });
            }

            await reader.NextResultAsync();

            var album = new AlbumPreviewViewModel
            {
                Artists = artistNames,
                Id = id,
                Cover = cover,
                Title = albumName,
                IsFavorite = isFavorite,
                IsDownloaded = false,
                IsDownloading = string.IsNullOrEmpty(onlineUrl) == false,
                DateAdded = dateAdded,
                YearReleased = yearReleased,
                AlbumType = albumType
            };
            allAlbums.Add(album);
        }
        
        return new AllAlbumsPreviewViewModel
        {
            Albums = allAlbums
        };
    }
}