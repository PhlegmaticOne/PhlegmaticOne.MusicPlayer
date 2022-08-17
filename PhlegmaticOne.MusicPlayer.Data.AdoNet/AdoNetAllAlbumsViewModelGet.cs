using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllAlbumsViewModelGet : AdoNetViewModelGetBase<AllAlbumsPreviewViewModel>
{
    public AdoNetAllAlbumsViewModelGet(ISqlClient sqlClient) : base(sqlClient, "Get_All_Albums_Preview") { }

    protected override async Task<AllAlbumsPreviewViewModel> Create(SqlDataReader reader)
    {
        var allAlbums = new List<AlbumPreviewViewModel>();

        while (await reader.ReadAsync())
        {
            var id = await reader.GetFieldValueAsync<Guid>(0);
            var yearReleased = await reader.GetFieldValueAsync<int>(1);
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


            var artistNames = new List<string>();
            while (await reader.ReadAsync())
            {
                var artistName = await reader.GetFieldValueAsync<string>(1);
                artistNames.Add(artistName);
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
            };
            allAlbums.Add(album);
        }
        
        return new AllAlbumsPreviewViewModel
        {
            Albums = allAlbums
        };
    }
}