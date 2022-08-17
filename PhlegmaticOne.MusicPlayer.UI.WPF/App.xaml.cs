﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhlegmaticOne.DependencyInjectionFactoryExtension;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using Microsoft.Extensions.Logging;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ValueProviders;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Download;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.UI;

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
    private static ILoggerFactory _debugFactory = LoggerFactory.Create(builder =>
    {
        builder.ClearProviders();
        builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name)
            .AddDebug();
    });
    private static IHostBuilder ConfigureServices()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["sql-connection-string"].ConnectionString;

        var hostBuilder = new HostBuilder()
            .ConfigureServices((builder, services) =>
             {
                 services.AddDbContext<ApplicationDbContext>(b =>
                 {
                     b.UseLoggerFactory(_debugFactory);
                     //b.UseInMemoryDatabase("MEMORY");
                     b.UseSqlServer(connectionString);
                 });

                 services.AddViewModelGetters(typeof(SqlClientSingleton).Assembly);
                 services.AddValueProvider<TrackBaseViewModel>();
                 services.AddValueProvider<CollectionBaseViewModel>();

                 services.AddSingleton<IUIThreadInvokerService, WpfUIThreadInvokerService>();
                 services.AddSingleton<IConnectionStringGetter, ConfigurationConnectionStringGetter>();
                 services.AddSingleton<ISqlClient, SqlClientSingleton>();

                 services.AddScoped<ReloadViewModelBase<AlbumsCollectionViewModel>, ReloadCollectionViewModel>();
                 services.AddScoped<ReloadViewModelBase<ArtistsCollectionViewModel>, ReloadArtistsViewModel>();
                 services.AddScoped<SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>, SortAlbumsViewModel>();
                 services.AddScoped<SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>, SortArtistsViewModel>();

                 services.AddSingleton<ILanguageProvider, LanguageProvider>();
                 services.AddSingleton<ILocalizeValuesGetter, LocalizeValuesGetter>();

                 services.AddSingleton<IHttpInfoGetter<Album>, MusifyAlbumInfoGetter>();

                 services.AddSingleton<IDownloadSettings, DownloadSettings>();
                 services.AddSingleton<IPlayer, CustomMusicPlayer>();
                 services.AddSingleton<IPlayerVolumeService, PlayerVolumeService>();
                 services.AddScoped<IDownloader, HttpDownloader>();
                 services.AddSingleton<INavigationHistory, NavigationHistory>();
                 services.AddSingleton<IObservableQueue<TrackBaseViewModel>, ObservableQueue<TrackBaseViewModel>>();

                 services.AddSingleton<IPlayerService, PlayerService>();
                 services.AddSingleton<IDownloadService<ActiveAlbumViewModel>, AlbumDownloadService>();
                 services.AddSingleton<ILocalizationService, LocalizationService>();
                 services.AddSingleton<INavigationService, NavigationService>();


                 services.AddDependencyFactory<HomeViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<PlayerViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<NavigationViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<AddingNewAlbumViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<ArtistsCollectionViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<AlbumsCollectionViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<DownloadedTracksViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<MainViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<PlaylistsViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<SettingsViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<TracksViewModel>(ServiceLifetime.Singleton);
                 services.AddDependencyFactory<SongQueueViewModel>(ServiceLifetime.Singleton);


                 services.AddSingleton<IMusicViewModelsFactory<AlbumPreviewViewModel, AlbumViewModel>, ActiveAlbumViewModelFactory>();
                 services.AddSingleton<IMusicViewModelsFactory<EntityBaseViewModel, SongQueueViewModel>, SongQueueViewModelFactory>();
                 services.AddSingleton<IMusicViewModelsFactory<CollectionLinkViewModel, AlbumViewModel>, CollectionLinkToAlbumNavigation>();
                 services.AddSingleton<MusicNavigation<AlbumPreviewViewModel, AlbumViewModel>>();
                 services.AddSingleton<MusicNavigation<EntityBaseViewModel, SongQueueViewModel>>();
                 services.AddSingleton<MusicNavigation<CollectionLinkViewModel, AlbumViewModel>>();

                 services.AddSingleton<IViewModelFactoryService, ViewModelFactoryService>();

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