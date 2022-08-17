using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public interface IEntityCollectionGetService
{
    public Task<T> GetEntityCollectionAsync<T>() where T : EntityBaseViewModel, IEntityCollection;
}