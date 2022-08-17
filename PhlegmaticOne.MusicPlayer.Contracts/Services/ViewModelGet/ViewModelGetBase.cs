using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public abstract class ViewModelGetBase<T> : IEntityCollectionGet<T> where T : EntityBaseViewModel, IEntityCollection
{
    public abstract Task<T> GetAsync();
    public async Task<object> Get() => await GetAsync();
}