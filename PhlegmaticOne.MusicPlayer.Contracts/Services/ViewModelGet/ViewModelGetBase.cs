using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public abstract class ViewModelGetBase<T> : IEntityCollectionGet<T> where T : EntityBaseViewModel, IEntityCollection
{
    public abstract Task<T> GetAsync();
    public async Task<object> Get() => await GetAsync();
}