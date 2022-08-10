using AutoMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

public class GenreMappingConfiguration : Profile
{
    public GenreMappingConfiguration()
    {
        CreateMap<Genre, GenreEntityViewModel>().ReverseMap();
    }
}