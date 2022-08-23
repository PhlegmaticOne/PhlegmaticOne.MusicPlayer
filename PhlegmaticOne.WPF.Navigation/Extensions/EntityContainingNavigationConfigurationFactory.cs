using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.WPF.Navigation.Extensions;

internal class EntityContainingNavigationConfigurationFactory : NavigationConfigurationFactory
{
    internal EntityContainingNavigationConfigurationFactory(Func<ApplicationBaseViewModel> factory, Type viewModelType) : base(factory, viewModelType) { }
}