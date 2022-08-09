using System;
using System.Linq;
using System.Windows;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AddingNewAlbumViewModel : BaseViewModel
{
    private readonly IHttpInfoGetter<Album> _albumInfoGetter;
    private readonly IUnitOfWork _unitOfWork;
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

    public AddingNewAlbumViewModel(IHttpInfoGetter<Album> albumInfoGetter, IUnitOfWork unitOfWork)
    {
        _albumInfoGetter = albumInfoGetter;
        _unitOfWork = unitOfWork;
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
        var repository = _unitOfWork.GetRepository<Album>();

        var existing = await repository.GetAllAsync(
            include: i => i.Include(a => a.Artists),
            predicate: p => p.Title == CurrentAlbum.Title);

        if (existing.Any(p => p.Artists.Except(CurrentAlbum.Artists).Any() == false))
        {
            MessageBox.Show("Same album has already been added");
            return;
        }
        CurrentAlbum.DateAdded = DateTime.Now;
        await repository.InsertAsync(CurrentAlbum!);
        await _unitOfWork.SaveChangesAsync();
        Clear(null);
    }

    private void Clear(object? parameter)
    {
        Url = string.Empty;
        CurrentAlbum = null;
    }
}