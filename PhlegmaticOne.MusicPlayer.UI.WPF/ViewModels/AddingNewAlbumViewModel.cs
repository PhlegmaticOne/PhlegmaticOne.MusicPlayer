using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AddingNewAlbumViewModel : ApplicationBaseViewModel
{
    private readonly IHttpInfoGetter<Album> _albumInfoGetter;
    private readonly ApplicationDbContext _dbContext;
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

    public AddingNewAlbumViewModel(IHttpInfoGetter<Album> albumInfoGetter, ApplicationDbContext dbContext)
    {
        _albumInfoGetter = albumInfoGetter;
        _dbContext = dbContext;
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
            await UIThreadInvoker.InvokeAsync(async () =>
            {
                CurrentAlbum = await _albumInfoGetter.GetInfoAsync(Url);
            });
        }
        catch
        {
            MessageBox.Show("Something went wrong. Please retype your link");
        }
    }

    private async void AddToCollection(object? parameter)
    {
        await Task.Run(async () =>
        {
            CurrentAlbum.DateAdded = DateTime.Now;

            var names = CurrentAlbum.Artists.Select(x => x.Name);
            var artists = await _dbContext.Set<Artist>()
                .Include(x => x.Albums)
                .Where(x => names.Contains(x.Name))
                .ToListAsync();

            CurrentAlbum.DateAdded = DateTime.Now;

            if (artists.Any())
            {
                foreach (var artist in artists)
                {
                    artist.Albums.Add(CurrentAlbum);
                }
            }
            else
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