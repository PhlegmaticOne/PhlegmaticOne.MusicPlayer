using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Tests.TestEntities;

public class TransitToEntityViewModelFactory : NavigationFactoryBase<TestEntityTransitViewModel, TestEntityViewModel>
{
    public override Task<TestEntityViewModel> CreateViewModelAsync(TestEntityTransitViewModel entityViewModel)
    {
        return Task.FromResult(new TestEntityViewModel()
        {
            Id = entityViewModel.Id
        });
    }
}