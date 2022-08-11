using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class TracksViewModel : BaseViewModel
{
    private readonly ApplicationDbContext _dbContext;
    public ObservableCollection<Song> Songs { get; set; }
    public TracksViewModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        //var albums = _dbContext.Set<Album>();
        //var songs = albums
        //    .AsNoTracking()
        //    .Include(x => x.AlbumCover)
        //    .Include(x => x.Artists)
        //    .Include(x => x.Songs)
        //    .Select(x => x.Songs)
        //    .ToList();
    }
}