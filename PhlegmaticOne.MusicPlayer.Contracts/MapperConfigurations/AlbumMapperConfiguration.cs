using AutoMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

public class AlbumMapperConfiguration : Profile
{
    public AlbumMapperConfiguration()
    {
        CreateMap<CollectionBase, CollectionDisplay>();


        CreateMap<Album, AlbumEntityViewModel>()
            .ForMember(x => x.IsDownloaded, o => o.Ignore());
        CreateMap<AlbumEntityViewModel, Album>();
    }
}