using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services;

public interface IViewModelGet<T> : IViewModelGet where T : EntityBaseViewModel
{
    public Task<T> GetAsync(Guid id);
}

public interface IViewModelGet
{
    public Task<object> Get(Guid id);
}