using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhlegmaticOne.DependencyInjectionFactoryExtension;
using PhlegmaticOne.MusicPlayer.Core.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.LanguagesSettings;
using PhlegmaticOne.MusicPlayer.UI.WPF.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Players;
using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

namespace PhlegmaticOne.MusicPlayer.UI.WPF;

public partial class App
{
    private IHost _host = null!;

    public static readonly IList<CultureInfo> SupportedCultures = new List<CultureInfo>();
    public static event EventHandler? LanguageChanged;
    internal static ResourceDictionary CurrentResourceDictionary { get; private set; } = null!;
    internal static CultureInfo Language
    {
        get => Thread.CurrentThread.CurrentUICulture;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Name == Thread.CurrentThread.CurrentUICulture.Name)
            {
                return;
            }
            Thread.CurrentThread.CurrentUICulture = value;
            var resourceDictionary = new ResourceDictionary
            {
                Source = value.Name == "en-US" ? 
                    new Uri("Resources/lang.xaml", UriKind.Relative) :
                    new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative)
            };
            var oldDictionary = Current.Resources.MergedDictionaries.FirstOrDefault(d => 
                d.Source.OriginalString.StartsWith("Resources/lang."));
            if (oldDictionary is not null)
            {
                var dictionaryIndex = Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                Current.Resources.MergedDictionaries.Remove(oldDictionary);
                Current.Resources.MergedDictionaries.Insert(dictionaryIndex, resourceDictionary);
            }
            else
            {
                Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            CurrentResourceDictionary = resourceDictionary;
            Settings.Default.DefaultLanguage = value.Name;
            Settings.Default.Save();
            LanguageChanged?.Invoke(Current, EventArgs.Empty);
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Language = new CultureInfo(Settings.Default.DefaultLanguage);
        SetResourceDictionary();
        _host = ConfigureServices().Build();
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        
    }

    private static IHostBuilder ConfigureServices()
    {
       // var connectionString = ConfigurationManager.ConnectionStrings["sql-connection-string"].ConnectionString;
        var hostBuilder = new HostBuilder()
            .ConfigureServices((builder, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("MEMORY"));
                services.AddUnitOfWork<ApplicationDbContext>();
                services.AddSingleton<ILanguageProvider, LanguageProvider>();
                services.AddSingleton<IAlbumFeaturesProvider, AlbumFeaturesProvider>();
                services.AddSingleton<ILocalizeValuesGetter, LocalizeValuesGetter>();
                services.AddSingleton<IHttpInfoGetter<Album>, MusifyAlbumInfoGetter>();
                services.AddSingleton<IDownloadSettings, DownloadSettings>();
                services.AddSingleton<ISortOptionsProvider, SortOptionsProvider>();
                services.AddSingleton<IPlayer, OnlinePlayer>();
                services.AddSingleton<MusicNavigationBase<Album>, AlbumsNavigation>();
                services.AddDependencyFactory<HomeViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<AddingNewAlbumViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<ArtistsViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<CollectionViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<DownloadedTracksViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<MainViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<PlaylistsViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<SettingsViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<TracksViewModel>(ServiceLifetime.Singleton);

                services.AddDependencyFactory<PlayersFactory>(ServiceLifetime.Singleton);

                services.AddSingleton<INavigationHistory, NavigationHistory>();
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<MainWindow>();
            });
        return hostBuilder;
    }

    private void SetResourceDictionary()
    {
        var culture = Thread.CurrentThread.CurrentUICulture;
        var resourceDictionary = new ResourceDictionary
        {
            Source = culture.Name == "en-US" ?
                new Uri("Resources/lang.xaml", UriKind.Relative) :
                new Uri($"Resources/lang.{culture.Name}.xaml", UriKind.Relative)
        };
        CurrentResourceDictionary = resourceDictionary;
    }
}