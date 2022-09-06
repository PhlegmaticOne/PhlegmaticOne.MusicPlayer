using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using System.Linq.Expressions;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;

public static class ArtistExpressionsConverter
{
    internal static Expression<Func<Artist, bool>>? ConvertToSelectExpression(Func<ArtistPreviewViewModel, bool>? selectFunc) =>
        selectFunc switch
        {
            Func<object, bool> _ => null,
            Func<IIsFavorite, bool> _ => a => a.IsFavorite,
            _ => null
        };
    internal static Expression<Func<Artist, object>>? ConvertToSortExpression(
        Func<ArtistPreviewViewModel, object>? sortFunc) =>
        sortFunc switch
        {
            Func<IHaveTitle, object> _ => a => a.Title,
            _ => null
        };
}