using System.Windows.Input;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Moq;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Models.Base;

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
    private AdoNetArtistsPagedListGetBase _adoNetAllArtistsViewModelGet;

    private EFAllAlbumsViewModelGet _efAlbumsViewModelGet;
    private AdoNetAlbumsPagedListGetBase _adoNetAllAlbumsViewModelGet;

    private EFAllTracksViewModelGet _efTracksViewModelGet;
    private AdoNetTracksPagedListGetBase _adoNetAllTracksViewModelGet;
    [GlobalSetup]
    public void Setup()
    {
        var connectionStringGetter = new Mock<IConnectionStringGetter>();
        connectionStringGetter.Setup(x => x.GetConnectionString()).Returns(_connectionString);

        var entityActionsProvider = new Mock<IEntityActionsProvider<TrackBaseViewModel>>();
        entityActionsProvider.Setup(x => x.GetActions(It.IsAny<TrackBaseViewModel>()))
            .Returns(new Dictionary<string, ICommand>());
        var dbContext = new ApplicationDbContext();
        var sqlClient = new SqlClientSingleton(connectionStringGetter.Object);
        _efAllArtistsViewModelGet = new EFAllArtistsViewModelGet(dbContext);
        _adoNetAllArtistsViewModelGet = new AdoNetArtistsPagedListGetBase(sqlClient);

        _efAlbumsViewModelGet = new EFAllAlbumsViewModelGet(dbContext);
        _adoNetAllAlbumsViewModelGet = new AdoNetAlbumsPagedListGetBase(sqlClient);

        _efTracksViewModelGet = new EFAllTracksViewModelGet(dbContext);
        _adoNetAllTracksViewModelGet = new AdoNetTracksPagedListGetBase(sqlClient, entityActionsProvider.Object);
    }


    [Benchmark(Description = "Create AllArtistsPreviewViewModel from EF")]
    public async Task CreateAllArtistsPreviewViewModelFromEF()
    {
        await _efAllArtistsViewModelGet.GetPagedListAsync(0, 0);
    }


    [Benchmark(Description = "Create AllArtistsPreviewViewModel from ADO.NET")]
    public async Task CreateAllArtistsPreviewViewModelFromAdoNet()
    {
        await _adoNetAllArtistsViewModelGet.GetPagedListAsync(0, 0);
    }

    [Benchmark(Description = "Create AllAlbumsPreviewViewModel from EF")]
    public async Task CreateAllAlbumsPreviewViewModelFromEF()
    {
        await _efAlbumsViewModelGet.GetPagedListAsync(0, 0);
    }


    [Benchmark(Description = "Create AllAlbumsPreviewViewModel from ADO.NET")]
    public async Task CreateAllAlbumsPreviewViewModelFromAdoNet()
    {
        await _adoNetAllAlbumsViewModelGet.GetPagedListAsync(0, 0);
    }


    [Benchmark(Description = "Create AllTracksViewModel from EF")]
    public async Task CreateAllTracksViewModelFromEF()
    {
        await _efTracksViewModelGet.GetPagedListAsync(0, 0);
    }


    [Benchmark(Description = "Create AllTracksViewModel from ADO.NET")]
    public async Task CreateAllTracksViewModelFromAdoNet()
    {
        await _adoNetAllTracksViewModelGet.GetPagedListAsync(0, 0);
    }
}