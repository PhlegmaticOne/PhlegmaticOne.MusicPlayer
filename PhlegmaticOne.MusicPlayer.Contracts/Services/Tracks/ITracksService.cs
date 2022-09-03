using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Tracks;

public interface ITracksService
{
    Task<ICollection<TrackBaseViewModel>> GetTracksAsync(bool isFavorite);
}