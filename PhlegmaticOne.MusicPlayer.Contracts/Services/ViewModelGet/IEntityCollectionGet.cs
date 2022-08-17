using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public interface IEntityCollectionGet<T> : IEntityCollectionGet where T : EntityBaseViewModel, IEntityCollection
{
    public Task<T> GetAsync();
}

public interface IEntityCollectionGet
{
    public Task<object> Get();
}