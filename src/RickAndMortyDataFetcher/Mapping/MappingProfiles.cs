using AutoMapper;
using RickAndMortyDataFetcher.DTOs;
using RickAndMortyDataFetcher.Entities;

namespace RickAndMortyDataFetcher.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CharacterDto, Character>()
            .ForMember(entity => entity.LocationName, dto => dto.MapFrom(a => a.Location != null ? a.Location.Name : ""))
            .ForMember(entity => entity.LocationUrl, dto => dto.MapFrom(a => a.Location != null ? a.Location.Url : ""))
            .ForMember(entity => entity.OriginName, dto => dto.MapFrom(a => a.Origin != null ? a.Origin.Name : ""))
            .ForMember(entity => entity.OriginUrl, dto => dto.MapFrom(a => a.Origin != null ? a.Origin.Url : ""))
            .ForMember(entity => entity.CharacterEpisodes, dto => dto.MapFrom(a => a.Episode));

        CreateMap<string, CharacterEpisode>()
            .ForMember(entity => entity.EpisodeUrl, dto => dto.MapFrom(a => a));
    }
}
