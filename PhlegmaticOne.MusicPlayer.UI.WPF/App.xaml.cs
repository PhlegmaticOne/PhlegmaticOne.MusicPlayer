using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhlegmaticOne.DependencyInjectionFactoryExtension;
using PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.LanguagesSettings;
using PhlegmaticOne.MusicPlayer.UI.WPF.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

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
                 services.AddAutoMapper(typeof(MapperConfig).Assembly);

                 services.AddScoped<ReloadViewModelBase<AlbumsCollectionViewModel>, ReloadCollectionViewModel>();
                 services.AddScoped<SortViewModelBase<AlbumsCollectionViewModel, AlbumEntityViewModel>, SortAlbumsViewModel>();

                 services.AddSingleton<IValueProvider<SongEntityViewModel>, ValueProvider<SongEntityViewModel>>();
                 services.AddSingleton<IValueProvider<CollectionBaseViewModel>, ValueProvider<CollectionBaseViewModel>>();
                 services.AddSingleton<ILanguageProvider, LanguageProvider>();
                 services.AddSingleton<IAlbumFeaturesProvider, AlbumFeaturesProvider>();
                 services.AddSingleton<ILocalizeValuesGetter, LocalizeValuesGetter>();

                 services.AddSingleton<IHttpInfoGetter<Album>, MusifyAlbumInfoGetter>();

                 services.AddSingleton<IDownloadSettings, DownloadSettings>();
                 services.AddSingleton<IPlayer, CustomMusicPlayer>();
                 services.AddScoped<IDownloader, HttpDownloader>();
                 services.AddSingleton<IObservableQueue<SongEntityViewModel>, ObservableQueue<SongEntityViewModel>>();

                 services.AddSingleton<IPlayerService, PlayerService>();
                 services.AddSingleton<IDownloadService<AlbumEntityViewModel>, AlbumDownloadService>();


                 services.AddDependencyFactory<HomeViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<PlayerViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<AddingNewAlbumViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<ArtistsViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<AlbumsCollectionViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<DownloadedTracksViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<MainViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<PlaylistsViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<SettingsViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<TracksViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<SongQueueViewModel>(ServiceLifetime.Singleton);

                 services.AddSingleton<INavigationHistory, NavigationHistory>();
                 services.AddSingleton<INavigator, Navigator>();
                 services.AddSingleton<MusicNavigationBase<AlbumEntityViewModel>, AlbumsNavigation>();

                 services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                 services.AddSingleton<ISongQueueViewModelFactory, SongQueueViewModelFactory>();

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