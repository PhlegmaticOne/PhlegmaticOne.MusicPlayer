using Microsoft.Extensions.DependencyInjection;
using PhlegmaticOne.MusicPlayer.Tests.TestEntities;
using PhlegmaticOne.WPF.Navigation;
using PhlegmaticOne.WPF.Navigation.ChainNavigation;
using PhlegmaticOne.WPF.Navigation.Extensions;

namespace PhlegmaticOne.MusicPlayer.Tests;

public class NavigationTests
{
    [Fact]
    public void Should_Not_Throw_Exceptions()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddNavigation()
            .UsingApplicationViewModelsFrom(typeof(TestEntityViewModel).Assembly);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var navigationService = serviceProvider.GetRequiredService<INavigationService>();

        navigationService.NavigateTo<TestApplicationViewModel>();
        Assert.NotNull(navigationService);
    }


    [Fact]
    public void Should_Not_Throw_Exceptions_Entity()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddChainNavigation()
            .UsingApplicationViewModelsFrom(typeof(TestEntityViewModel).Assembly)
            .AddEntityContainingNavigation()
            .UsingApplicationViewModelsFrom(typeof(TestEntityViewModel).Assembly)
            .UsingNavigationFactoriesFrom(typeof(TestEntityViewModel).Assembly);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var navigationService = serviceProvider.GetRequiredService<IEntityContainingViewModelsNavigationService>();

        var from = new TestEntityTransitViewModel();
        navigationService.From<TestEntityTransitViewModel, TestEntityViewModel>()
            .NavigateAsync<TestEntityContainingViewModel>(from);

        Assert.NotNull(navigationService);
    }

    [Fact]
    public void Should_Not_Throw_Exceptions_Chain()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddNavigation()
            .UsingApplicationViewModelsFrom(typeof(TestEntityViewModel).Assembly);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var navigationService = serviceProvider.GetRequiredService<IChainNavigationService>();

        navigationService.NavigateTo<TestApplicationViewModel>();

        Assert.NotNull(navigationService);
    }

    [Fact]
    public void Should_Be_Of_Type_IChainedNavigation()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddNavigation()
            .UsingApplicationViewModelsFrom(typeof(TestEntityViewModel).Assembly);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var navigationService = serviceProvider.GetRequiredService<INavigationService>();

    }
}