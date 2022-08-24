using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public interface IEntityCollectionGetService
{
    public Task<T> GetEntityCollectionAsync<T>() where T : EntityBaseViewModel, IEntityCollection;
}