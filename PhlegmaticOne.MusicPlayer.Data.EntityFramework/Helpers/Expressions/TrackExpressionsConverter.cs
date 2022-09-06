using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using System.Linq.Expressions;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;

public static class TrackExpressionsConverter
{
    internal static Expression<Func<Song, bool>>? ConvertToSelectExpression(Func<TrackBaseViewModel, bool>? selectFunc) =>
        selectFunc switch
        {
            Func<object, bool> _ => null,
            Func<IIsFavorite, bool> _ => a => a.IsFavorite,
            _ => null
        };

    internal static Expression<Func<Song, object>>? ConvertToSortExpression(
        Func<TrackBaseViewModel, object>? sortFunc) =>
        sortFunc switch
        {
            Func<IHaveTitle, object> _ => a => a.Title,
            Func<IHaveDuration, object> _ => a => a.Duration,
            Func<IHaveTimePlayed, object> _ => a => a.TimePlayed,
            Func<IHaveArtistName, object> _ => a => a.Artists.First().Title,
            _ => null
        };
}