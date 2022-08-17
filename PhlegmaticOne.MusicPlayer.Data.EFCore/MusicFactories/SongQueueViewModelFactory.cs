using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class SongQueueViewModelFactory : IMusicViewModelsFactory<EntityBaseViewModel, SongQueueViewModel>
{
    private readonly IPlayerService _playerService;

    public SongQueueViewModelFactory(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    public Task<SongQueueViewModel> CreateViewModelAsync(EntityBaseViewModel entity)
    {
        return Task.FromResult(new SongQueueViewModel(_playerService));
    }
}