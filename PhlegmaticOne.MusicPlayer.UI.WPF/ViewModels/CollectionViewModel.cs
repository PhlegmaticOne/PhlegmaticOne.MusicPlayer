using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class CollectionViewModel : BaseViewModel
{
    public MusicNavigationBase<Album> MusicNavigation { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    public ObservableCollection<Album> Albums { get; set; } = new();
    public ObservableCollection<SortDescription> SortOptions { get; set; } = new();

    public CollectionViewModel(IUnitOfWork unitOfWork,
        ISortOptionsProvider sortOptionsProvider,
        MusicNavigationBase<Album> musicNavigation)
    {
        MusicNavigation = musicNavigation;
        _unitOfWork = unitOfWork;
        UpdateCommand = new(LoadAlbums, _ => true);
        SortCommand = new(SortAlbums, _ => true);
        foreach (var sortDescription in sortOptionsProvider.GetSortDescriptions())
        {
            SortOptions.Add(sortDescription);
        }
        LoadAlbums();
    }
    public DelegateCommand UpdateCommand { get; set; }
    public DelegateCommand SortCommand { get; set; }
    private async void LoadAlbums(object? b = null)
    {
        var repository = _unitOfWork.GetRepository<Album>();
        var albums = await repository.GetAllAsync(
            include: include => include
                .Include(p => p.Artists)
                .Include(p => p.AlbumCover)
                .Include(p => p.Songs)
        );

        await UpdateAlbums(albums);
    }

    private async void SortAlbums(object? b = null)
    {
        if(b is not SortType sortType) return;

        List<Album> sortedAlbums;
        switch (sortType)
        {
            case SortType.Title:
                sortedAlbums = Albums.OrderBy(a => a.Title).ToList();
                break;
            case SortType.DateAdded:
                sortedAlbums = Albums.OrderByDescending(x => x.DateAdded).ToList();
                break;
            case SortType.ArtistName:
                sortedAlbums = Albums.OrderBy(x => x.Artists.First().Name).ToList();
                break;
            default: return;
        }

        await UpdateAlbums(sortedAlbums);
    }

    private async Task UpdateAlbums(IList<Album> newAlbums)
    {
        Albums.Clear();

        foreach (var album in newAlbums)
        {
            await UIThreadInvoker.InvokeAsync(() => Albums.Add(album));
        }
    }
}