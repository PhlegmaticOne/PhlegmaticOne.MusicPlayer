using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAlbumPreviewViewModelGet : AdoNetViewModelGetBase<AlbumPreviewViewModel>
{
    public AdoNetAlbumPreviewViewModelGet(ISqlClient sqlClient) : base(sqlClient, "Get_Album_Preview") { }

    protected override string ParameterName => "@albumId";
    protected override async Task<AlbumPreviewViewModel> Create(SqlDataReader reader, Guid id)
    {

        await reader.ReadAsync();
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


        return new AlbumPreviewViewModel
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
    }
}