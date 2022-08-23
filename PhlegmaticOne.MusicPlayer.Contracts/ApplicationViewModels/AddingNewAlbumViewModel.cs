using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class AddingNewAlbumViewModel : ApplicationBaseViewModel
{
    private readonly IHttpInfoGetter<Album> _albumInfoGetter;
    private readonly ApplicationDbContext _dbContext;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    private string _url;
    private Album? _currentAlbum;

    public Album? CurrentAlbum
    {
        get => _currentAlbum;
        set
        {
            _currentAlbum = value;
            AddToCollectionCommand.RaiseCanExecute();
            ClearCommand.RaiseCanExecute();
        }
    }

    public string Url
    {
        get => _url;
        set
        {
            _url = value;
            GetAlbumInfoCommand.RaiseCanExecute();
        }
    }

    public AddingNewAlbumViewModel(IHttpInfoGetter<Album> albumInfoGetter, ApplicationDbContext dbContext, IUIThreadInvokerService uiThreadInvokerService)
    {
        _albumInfoGetter = albumInfoGetter;
        _dbContext = dbContext;
        _uiThreadInvokerService = uiThreadInvokerService;
        GetAlbumInfoCommand = new(GetAlbumInfo, _ => string.IsNullOrEmpty(Url) == false);
        AddToCollectionCommand = new(AddToCollection, _ => CurrentAlbum is not null);
        ClearCommand = new(Clear, _ => CurrentAlbum is not null);
    }

    public DelegateCommand GetAlbumInfoCommand { get; set; }
    public DelegateCommand AddToCollectionCommand { get; set; }
    public DelegateCommand ClearCommand { get; set; }

    private async void GetAlbumInfo(object? parameter)
    {
        try
        {
            await _uiThreadInvokerService.InvokeAsync(async () =>
            {
                CurrentAlbum = await _albumInfoGetter.GetInfoAsync(Url);
            });
        }
        catch
        {
            //MessageBox.Show("Something went wrong. Please retype your link");
        }
    }

    private async void AddToCollection(object? parameter)
    {
        await Task.Run(async () =>
        {
            CurrentAlbum.DateAdded = DateTime.Now;

            var artistNames = CurrentAlbum.Artists.Select(x => x.Name);
            var genreNames = CurrentAlbum.Genres.Select(x => x.Name);

            var artists = await _dbContext.Set<Artist>()
                .Include(x => x.Albums)
                .Where(x => artistNames.Contains(x.Name))
                .ToListAsync();

            var genres = await _dbContext.Set<Genre>()
                .Include(x => x.Albums)
                .Where(x => genreNames.Contains(x.Name))
                .ToListAsync();


            CurrentAlbum.Artists = CurrentAlbum.Artists.Except(artists).ToList();
            CurrentAlbum.Genres = CurrentAlbum.Genres.Except(genres).ToList();
            CurrentAlbum.DateAdded = DateTime.Now;

            var isNew = true;

            foreach (var artist in artists)
            {
                artist.Albums.Add(CurrentAlbum);
                isNew = false;
            }

            foreach (var genre in genres)
            {
                genre.Albums.Add(CurrentAlbum);
                isNew = false;
            }

            if (isNew)
            {
                await _dbContext.Set<Album>().AddAsync(CurrentAlbum!);
            }

            await _dbContext.SaveChangesAsync();
        });
        Clear(null);
    }

    private void Clear(object? parameter)
    {
        Url = string.Empty;
        CurrentAlbum = null;
    }
}