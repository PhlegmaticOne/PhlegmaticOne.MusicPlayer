namespace PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;

public interface IHttpInfoGetter<T> where T: class
{
    public Task<T> GetInfoAsync(string uri);
}