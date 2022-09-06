using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using MediatR;
using Microsoft.Extensions.Logging;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Count;
using PhlegmaticOne.WPF.Navigation.Extensions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Logo;
using PhlegmaticOne.MusicPlayer.Data.Common.HandMappers;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Navigation;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Other.DownloadSongsFeature;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Other.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Save;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Count;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Download;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Like;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Save;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Download;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services.Logo;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.Players.Models;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.PlayerService.Extensions;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList.PageSizes;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList.Select;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;

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
    private static readonly ILoggerFactory _debugFactory = LoggerFactory.Create(builder =>
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

                 //services.AddMediatR(typeof(GenericGetPagedListQuery<>).Assembly);

                 services.AddCacheService();

                 services.AddHandMappers(typeof(AlbumPreviewToActiveViewModelMapper).Assembly);

                 services.AddPlayerService<TrackBaseViewModel>()
                     .UsingPlayer<NAudioMusicPlayer>();

                 services.AddChainNavigation()
                     .UsingApplicationViewModelsFrom(typeof(PlayerTrackableViewModel).Assembly)
                     .AddEntityContainingNavigation()
                     .UsingApplicationViewModelsFrom(typeof(PlayerTrackableViewModel).Assembly)
                     .UsingNavigationFactoriesFrom(typeof(EFActiveAlbumFromAlbumPreviewViewModelFactory).Assembly);


                 services.AddSingleton<ILikeService, EFLikeService>();

                 services.AddSingleton<IUiThreadInvokerService, WpfUIThreadInvokerService>();

                 #region PagedList

                 services.AddSingleton<ISortOptionsProvider<AlbumPreviewViewModel>, AlbumsSortOptionsProvider>();
                 services.AddSingleton<ISortOptionsProvider<ArtistPreviewViewModel>, ArtistsSortOptionsProvider>();
                 services.AddSingleton<ISortOptionsProvider<TrackBaseViewModel>, TracksSortOptionsProvider>();

                 services.AddSingleton<ISelectOptionsProvider<AlbumPreviewViewModel>, AlbumsSelectOptionsProvider>();
                 services.AddSingleton<ISelectOptionsProvider<ArtistPreviewViewModel>, ArtistsSelectOptionsProvider>();
                 services.AddSingleton<ISelectOptionsProvider<TrackBaseViewModel>, TracksSelectOptionsProvider>();

                 services.AddSingleton<IEntityPagedListGet<AlbumPreviewViewModel>, EFAllAlbumsViewModelGet>();
                 services.AddSingleton<IEntityPagedListGet<ArtistPreviewViewModel>, EFAllArtistsViewModelGet>();
                 services.AddSingleton<IEntityPagedListGet<TrackBaseViewModel>, EFAllTracksViewModelGet>();

                 services.AddSingleton<IGetEntitiesCountGetService<AlbumPreviewViewModel>, AlbumsEntityCountGetService>();
                 services.AddSingleton<IGetEntitiesCountGetService<ArtistPreviewViewModel>, ArtistsEntityCountGetService>();
                 services.AddSingleton<IGetEntitiesCountGetService<TrackBaseViewModel>, TracksEntityCountGetService>();

                 services.AddSingleton<IAvailablePageSizesProvider, AvailablePageSizesProvider>();

                 #endregion


                 services.AddSingleton<ILanguageProvider, LanguageProvider>();
                 services.AddSingleton<ILocalizeValuesGetter, LocalizeValuesGetter>();

                 services.AddSingleton<IHttpInfoGetter<Album>, MusifyAlbumInfoGetter>();

                 services.AddSingleton<IAlbumSaveService, AlbumSaveService>();
                 services.AddSingleton<IEntityActionsProvider<TrackBaseViewModel>, TrackActionsProvider>();

                 services.AddSingleton<ILocalSystemSettings, DownloadSettings>();
                 services.AddSingleton<IPlayerVolumeService, PlayerVolumeService>();
                 services.AddScoped<IDownloader, HttpDownloader>(); ;
                 services.AddScoped<ILogoProvider, WpfLogoProvider>(); ;

                 services.AddSingleton<IFileOperatingService<TrackBaseViewModel>, TrackFileOperatingService>();
                 services.AddSingleton<ILocalizationService, LocalizationService>();

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