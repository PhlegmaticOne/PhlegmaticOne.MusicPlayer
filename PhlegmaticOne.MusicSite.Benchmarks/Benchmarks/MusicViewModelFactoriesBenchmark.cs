using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Moq;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.EFCore.ViewModelGetters;

namespace PhlegmaticOne.MusicSite.Benchmarks.Benchmarks;

[Config(typeof(Config))]
public class MusicViewModelFactoriesBenchmark
{
    private static readonly string _connectionString =
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=music-player-db;Integrated Security=True;";
    private class Config : ManualConfig
    {
        public Config()
        {
            //AddLogger(ConsoleLogger.Default);
        }
    }
    private EFAllArtistsViewModelGet _efAllArtistsViewModelGet;
    private AdoNetAllArtistsViewModelGet _adoNetAllArtistsViewModelGet;

    private EFAllAlbumsViewModelGet _efAlbumsViewModelGet;
    private AdoNetAllAlbumsViewModelGet _adoNetAllAlbumsViewModelGet;

    private EFAllTracksViewModelGet _efTracksViewModelGet;
    private AdoNetAllTracksViewModelGet _adoNetAllTracksViewModelGet;
    [GlobalSetup]
    public void Setup()
    {
        var connectionStringGetter = new Mock<IConnectionStringGetter>();
        connectionStringGetter.Setup(x => x.GetConnectionString()).Returns(_connectionString);

        var cacheService = new Mock<ICacheService>();
        cacheService.Setup(x => x.ContainsKey<TrackBaseViewModel>(It.IsAny<Guid>())).Returns(false);

        var dbContext = new ApplicationDbContext();
        var sqlClient = new SqlClientSingleton(connectionStringGetter.Object);
        _efAllArtistsViewModelGet = new EFAllArtistsViewModelGet(dbContext);
        _adoNetAllArtistsViewModelGet = new AdoNetAllArtistsViewModelGet(sqlClient);

        _efAlbumsViewModelGet = new EFAllAlbumsViewModelGet(dbContext);
        _adoNetAllAlbumsViewModelGet = new AdoNetAllAlbumsViewModelGet(sqlClient);

        _efTracksViewModelGet = new EFAllTracksViewModelGet(dbContext);
        _adoNetAllTracksViewModelGet = new AdoNetAllTracksViewModelGet(sqlClient, cacheService.Object);
    }


    [Benchmark(Description = "Create AllArtistsPreviewViewModel from EF")]
    public async Task CreateAllArtistsPreviewViewModelFromEF()
    {
        await _efAllArtistsViewModelGet.GetAsync();
    }


    [Benchmark(Description = "Create AllArtistsPreviewViewModel from ADO.NET")]
    public async Task CreateAllArtistsPreviewViewModelFromAdoNet()
    {
        await _adoNetAllArtistsViewModelGet.GetAsync();
    }

    [Benchmark(Description = "Create AllAlbumsPreviewViewModel from EF")]
    public async Task CreateAllAlbumsPreviewViewModelFromEF()
    {
        await _efAlbumsViewModelGet.GetAsync();
    }


    [Benchmark(Description = "Create AllAlbumsPreviewViewModel from ADO.NET")]
    public async Task CreateAllAlbumsPreviewViewModelFromAdoNet()
    {
        await _adoNetAllAlbumsViewModelGet.GetAsync();
    }


    [Benchmark(Description = "Create AllTracksViewModel from EF")]
    public async Task CreateAllTracksViewModelFromEF()
    {
        await _efTracksViewModelGet.GetAsync();
    }


    [Benchmark(Description = "Create AllTracksViewModel from ADO.NET")]
    public async Task CreateAllTracksViewModelFromAdoNet()
    {
        await _adoNetAllTracksViewModelGet.GetAsync();
    }
}