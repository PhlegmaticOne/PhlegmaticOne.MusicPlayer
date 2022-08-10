using AutoMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

public class CollectionMapperConfiguration : Profile
{
    public CollectionMapperConfiguration()
    {
        CreateMap<CollectionBase, CollectionDisplay>();
        CreateMap<CollectionBaseViewModel, CollectionDisplay>();

        CreateMap<CollectionBase, CollectionBaseViewModel>()
            .Include<Album, AlbumEntityViewModel>()
            .Include<Playlist, PlaylistEntityViewModel>()
            .ForMember(x => x.IsDownloaded, o => o.Ignore())
            .ForMember(x => x.IsDownloading, o => o.Ignore());

        CreateMap<Album, AlbumEntityViewModel>();
        CreateMap<Playlist, PlaylistEntityViewModel>();
    }
}