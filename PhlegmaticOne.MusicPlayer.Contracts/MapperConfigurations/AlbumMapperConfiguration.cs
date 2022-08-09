using AutoMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

public class AlbumMapperConfiguration : Profile
{
    public AlbumMapperConfiguration()
    {
        CreateMap<CollectionBase, CollectionDisplay>();


        CreateMap<Album, AlbumEntityViewModel>();
        CreateMap<AlbumEntityViewModel, Album>();
    }
}