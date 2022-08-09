﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class CollectionViewModel : BaseViewModel
{
    public MusicNavigationBase<AlbumEntityViewModel> MusicNavigation { get; set; }
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ObservableCollection<AlbumEntityViewModel> Albums { get; set; } = new();
    public ObservableCollection<SortDescription> SortOptions { get; set; } = new();

    public CollectionViewModel(IUnitOfWork unitOfWork, IMapper mapper, ISortOptionsProvider sortOptionsProvider, MusicNavigationBase<AlbumEntityViewModel> musicNavigation)
    {
        MusicNavigation = musicNavigation;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        var mapped = _mapper.Map<IList<AlbumEntityViewModel>>(albums);

        await UpdateAlbums(mapped);
    }

    private async void SortAlbums(object? b = null)
    {
        if(b is not SortType sortType) return;

        List<AlbumEntityViewModel> sortedAlbums;
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

    private async Task UpdateAlbums(IList<AlbumEntityViewModel> newAlbums)
    {
        Albums.Clear();

        foreach (var album in newAlbums)
        {
            await UIThreadInvoker.InvokeAsync(() => Albums.Add(album));
        }
    }
}