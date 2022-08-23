using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public interface IEntityCollectionGetService
{
    public Task<T> GetEntityCollectionAsync<T>() where T : EntityBaseViewModel, IEntityCollection;
}