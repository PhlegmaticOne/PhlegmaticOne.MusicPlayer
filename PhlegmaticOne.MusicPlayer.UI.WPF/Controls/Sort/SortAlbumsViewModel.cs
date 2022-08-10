using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;

public class SortAlbumsViewModel : SortViewModelBase<AlbumsCollectionViewModel, AlbumEntityViewModel>
{
    public SortAlbumsViewModel(ILocalizeValuesGetter localizeValuesGetter) : base(localizeValuesGetter)
    {
    }

    protected override Dictionary<string, Func<IEnumerable<AlbumEntityViewModel>, IEnumerable<AlbumEntityViewModel>>> GetAvailableSorts()
    {
        return new()
        {
            {"By name", (albums) => albums.OrderBy(x => x.Title)}
        };
    }
}