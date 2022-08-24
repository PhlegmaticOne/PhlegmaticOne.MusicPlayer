using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public interface IEntityCollectionGet<T> : IEntityCollectionGet where T : EntityBaseViewModel, IEntityCollection
{
    public Task<T> GetAsync();
}

public interface IEntityCollectionGet
{
    public Task<object> Get();
}