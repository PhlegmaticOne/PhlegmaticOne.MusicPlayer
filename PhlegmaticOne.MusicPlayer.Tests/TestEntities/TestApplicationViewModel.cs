using PhlegmaticOne.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Tests.TestEntities;

public class TestApplicationViewModel : ApplicationBaseViewModel
{
    private readonly INavigationService _navigationService;

    public TestApplicationViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}