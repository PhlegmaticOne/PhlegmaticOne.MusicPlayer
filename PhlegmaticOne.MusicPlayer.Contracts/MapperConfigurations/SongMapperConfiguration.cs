using AutoMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

public class SongMapperConfiguration : Profile
{
    public SongMapperConfiguration()
    {
        CreateMap<Song, SongEntityViewModel>()
            .ForMember(x => x.IsDownloaded, o => o.Ignore())
            .ForMember(x => x.CurrentCollection, o => o.Ignore())
            .ForMember(x => x.IsDownloading, o => o.Ignore())
            .ForMember(x => x.Appearances, o => o.MapFrom(p => p.AlbumAppearances));

        CreateMap<SongEntityViewModel, Song>()
            .ForMember(x => x.AlbumAppearances, o => o.Ignore());
    }
}