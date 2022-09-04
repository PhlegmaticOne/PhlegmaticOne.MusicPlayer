namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Other.HttpInfoRetrieveFeature;

public interface IHttpInfoGetter<T> where T: class
{
    public Task<T> GetInfoAsync(string uri);
}