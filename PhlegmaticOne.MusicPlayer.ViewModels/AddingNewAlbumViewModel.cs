using System.Collections.ObjectModel;
using System.Windows.Forms;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Other.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Save;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels;

public class AddingNewAlbumViewModel : ApplicationBaseViewModel
{
    private readonly List<Artist> _currentAlbumNewArtists;
    private readonly IHttpInfoGetter<Album> _albumInfoGetter;
    private readonly IUiThreadInvokerService _uiThreadInvokerService;

    private string _url;
    private Album? _currentAlbum;
    private readonly IAlbumSaveService _albumSaveService;

    public Album? CurrentAlbum
    {
        get => _currentAlbum;
        set
        {
            _currentAlbum = value;
            AddToCollectionCommand.RaiseCanExecute();
            ClearCommand.RaiseCanExecute();
            ReplaceArtistCommand.RaiseCanExecute();
        }
    }
    public bool IsActive { get; private set; }
    public string Url
    {
        get => _url;
        set
        {
            _url = value;
            GetAlbumInfoCommand.RaiseCanExecute();
        }
    }
    public ObservableCollection<IGrouping<string, Artist>> ExistingArtists { get; set; }
    public AddingNewAlbumViewModel(IHttpInfoGetter<Album> albumInfoGetter,
        IUiThreadInvokerService uiThreadInvokerService,
        IAlbumSaveService albumSaveService)
    {
        _albumInfoGetter = albumInfoGetter;
        _uiThreadInvokerService = uiThreadInvokerService;
        _albumSaveService = albumSaveService;
        _url = string.Empty;
        ExistingArtists = new();
        _currentAlbumNewArtists = new();
        GetAlbumInfoCommand = RelayCommandFactory.CreateCommand(GetAlbumInfo, _ => string.IsNullOrEmpty(Url) == false);
        AddToCollectionCommand = RelayCommandFactory.CreateCommand(AddToCollection, _ => CurrentAlbum is not null);
        ClearCommand = RelayCommandFactory.CreateCommand(Clear, _ => CurrentAlbum is not null);
        ReplaceArtistCommand = RelayCommandFactory.CreateCommand(ReplaceArtist, _ => CurrentAlbum is not null);
    }

    public IRelayCommand GetAlbumInfoCommand { get; set; }
    public IRelayCommand AddToCollectionCommand { get; set; }
    public IRelayCommand ClearCommand { get; set; }
    public IRelayCommand ReplaceArtistCommand { get; }

    private void ReplaceArtist(object? parameter)
    {
        if (parameter is Artist artist)
        {
            var existingArtist = _currentAlbumNewArtists.First(x => x.Title == artist.Title);
            _currentAlbumNewArtists.Remove(existingArtist);
            _currentAlbumNewArtists.Add(artist);
        }
    }
    private async void GetAlbumInfo(object? parameter)
    {
        try
        {
            await _uiThreadInvokerService.InvokeAsync(async () =>
            {
                CurrentAlbum = await _albumInfoGetter.GetInfoAsync(Url);
                var albumExistingArtists = await _albumSaveService.GetExistingArtistsAsync(CurrentAlbum);

                foreach (var albumExistingArtist in albumExistingArtists)
                {
                    ExistingArtists.Add(albumExistingArtist);
                }
                _currentAlbumNewArtists.AddRange(CurrentAlbum.Artists);
                IsActive = true;
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
            var adapted = _albumSaveService.AdaptWithArtists(CurrentAlbum!, _currentAlbumNewArtists);
            await _albumSaveService.SaveAsync(adapted);
        });
        Clear(null);
    }

    private void Clear(object? parameter)
    {
        Url = string.Empty;
        CurrentAlbum = null;
        IsActive = false;
    }
}