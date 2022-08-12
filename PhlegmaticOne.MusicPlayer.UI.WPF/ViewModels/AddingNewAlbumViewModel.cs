using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;
using System.Linq;
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
            CurrentAlbum = await _albumInfoGetter.GetInfoAsync(Url);
        }
        catch
        {
            MessageBox.Show("Something went wrong. Please retype your link");
        }
    }

    private async void AddToCollection(object? parameter)
    {
        var albums = _dbContext.Set<Album>();
        var existing = await albums.AsNoTracking()
            .Include(x => x.Artists)
            .Where(x => x.Title == CurrentAlbum.Title)
            .ToListAsync();

        if (existing.Any(p => p.Artists.Except(CurrentAlbum.Artists).Any() == false))
        {
            MessageBox.Show("Same album has already been added");
            return;
        }

        CurrentAlbum.DateAdded = DateTime.Now;

        await albums.AddAsync(CurrentAlbum!);
        await _dbContext.SaveChangesAsync();

        Clear(null);
    }

    private void Clear(object? parameter)
    {
        Url = string.Empty;
        CurrentAlbum = null;
    }
}