using PhlegmaticOne.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Tests.TestEntities;

public class TestEntityContainingViewModel : ApplicationBaseViewModel, IEntityContainingViewModel<TestEntityViewModel>
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private readonly INavigationService _navigationService;
    public TestEntityViewModel Entity { get; set; }

    public TestEntityContainingViewModel(IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService, INavigationService navigationService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _navigationService = navigationService;
    }
}