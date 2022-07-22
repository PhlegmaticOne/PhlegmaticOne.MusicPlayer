namespace PhlegmaticOne.MusicPlayer.Core.HttpInfoRetrieveFeature;

public interface IHttpInfoGetter<T> where T: class
{
    public Task<T> GetInfoAsync(string uri);
}