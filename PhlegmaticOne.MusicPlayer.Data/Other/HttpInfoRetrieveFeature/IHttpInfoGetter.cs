namespace PhlegmaticOne.MusicPlayer.Data.Other.HttpInfoRetrieveFeature;

public interface IHttpInfoGetter<T> where T: class
{
    public Task<T> GetInfoAsync(string uri);
}