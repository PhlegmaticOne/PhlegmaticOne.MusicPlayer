using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhlegmaticOne.DependencyInjectionFactoryExtension;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.UI.WPF.ConfigurationSections.SupportedCulturesSection;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

namespace PhlegmaticOne.MusicPlayer.UI.WPF;

public partial class App
{
    private IHost _host = null!;

    public static readonly IList<CultureInfo> SupportedCultures = new List<CultureInfo>();
    public static event EventHandler? LanguageChanged;
    public static CultureInfo Language
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
            LanguageChanged?.Invoke(Current, EventArgs.Empty);
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        AddSupportedCultures();
        _host = ConfigureServices().Build();
        Language = new CultureInfo("ru-RU");
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
                services.AddDependencyFactory<HomeViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<AddingNewAlbumViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<ArtistsViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<CollectionViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<DownloadedTracksViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<MainViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<PlaylistsViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<SettingsViewModel>(ServiceLifetime.Singleton);
                services.AddDependencyFactory<TracksViewModel>(ServiceLifetime.Singleton);
                services.AddScoped<INavigator, Navigator>();
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<MainWindow>();
            });
        return hostBuilder;
    }

    private static void AddSupportedCultures()
    {
        var supportedCultures = (CultureConfigurationSection)ConfigurationManager.GetSection("supportedCultures");
        foreach (CultureElement cultureElement in supportedCultures.CultureCollection)
        {
            SupportedCultures.Add(new CultureInfo(cultureElement.Name));
        }
    }
}