using PhlegmaticOne.MusicPlayer.Core.HttpInfoRetrieveFeature;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AddingNewAlbumViewModel : BaseViewModel
{
    private readonly IHttpInfoGetter<Album> _albumInfoGetter;
    private string _url;
    public Album CurrentAlbum { get; set; }

    public string Url
    {
        get => _url;
        set
        {
            _url = value;
            GetAlbumInfoCommand.RaiseCanExecute();
        }
    }

    public AddingNewAlbumViewModel(IHttpInfoGetter<Album> albumInfoGetter)
    {
        _albumInfoGetter = albumInfoGetter;
        GetAlbumInfoCommand = new(GetAlbumInfo, _ => string.IsNullOrEmpty(Url) == false);
    }
    public DelegateCommand GetAlbumInfoCommand { get; set; }

    private async void GetAlbumInfo(object? parameter)
    {
        CurrentAlbum = await _albumInfoGetter.GetInfoAsync(Url);
    } 
}