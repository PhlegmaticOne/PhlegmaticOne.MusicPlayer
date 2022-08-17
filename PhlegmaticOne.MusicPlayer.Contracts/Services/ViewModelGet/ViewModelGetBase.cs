using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public abstract class ViewModelGetBase<T> : IViewModelGet<T> where T : EntityBaseViewModel
{
    public abstract Task<T> GetAsync(Guid id);

    public async Task<object> Get(Guid id)
    {
        return await GetAsync(id);
    }
}