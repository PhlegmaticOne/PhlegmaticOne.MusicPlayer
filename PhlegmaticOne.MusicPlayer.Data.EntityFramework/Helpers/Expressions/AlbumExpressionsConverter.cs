using System.Linq.Expressions;
using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;

internal static class AlbumExpressionsConverter
{
    internal static Expression<Func<Album, bool>>? ConvertToSelectExpression(Func<AlbumPreviewViewModel, bool>? selectFunc) =>
        selectFunc switch
        {
            Func<object, bool> _ => null,
            Func<IIsFavorite, bool> _ => a => a.IsFavorite,
            _ => null
        };

    internal static Expression<Func<Album, object>>? ConvertToSortExpression(
        Func<AlbumPreviewViewModel, object>? sortFunc) =>
        sortFunc switch
        {
            Func<IHaveDateAdded, object> _ => a => a.DateAdded,
            Func<IHaveYear, object> _ => a => a.YearReleased,
            Func<IHaveTitle, object> _ => a => a.Title,
            Func<IHaveArtistName, object> _ => a => a.Artists.First().Title,
            _ => null
        };
}