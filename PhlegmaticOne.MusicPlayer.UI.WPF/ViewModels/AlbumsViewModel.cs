using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumsViewModel : BaseViewModel
{
    public ReloadViewModelBase<AlbumsViewModel> ReloadViewModel { get; }
    public MusicNavigationBase<AlbumEntityViewModel> MusicNavigation { get; set; }
    public ObservableCollection<SortDescription> SortOptions { get; set; } = new();

    public ObservableCollection<AlbumEntityViewModel> Albums { get; set; } = new();


    public AlbumsViewModel(ReloadViewModelBase<AlbumsViewModel> reloadViewModel,  ISortOptionsProvider sortOptionsProvider, MusicNavigationBase<AlbumEntityViewModel> musicNavigation)
    {
        ReloadViewModel = reloadViewModel;
        MusicNavigation = musicNavigation;
        SortCommand = new(SortAlbums, _ => true);
        foreach (var sortDescription in sortOptionsProvider.GetSortDescriptions())
        {
            SortOptions.Add(sortDescription);
        }
        //LoadAlbums();
    }
    public DelegateCommand SortCommand { get; set; }

    private void LoadAlbums() => ReloadViewModel.ReloadCommand.Execute(this);
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

    internal async Task UpdateAlbums(IList<AlbumEntityViewModel> newAlbums)
    {
        await UIThreadInvoker.InvokeAsync(() =>
        {
            Albums.Clear();
            foreach (var album in newAlbums)
            {
                Albums.Add(album);
            }
        });
    }
}