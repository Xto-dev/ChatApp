using AutoMapper;
using ChatApp.Backend.Entities;
using ChatApp.Backend.Infrastructure.DTO;

namespace ChatApp.Backend.Infrastructure;

public class ResponseMessageProfile: Profile
{
    public ResponseMessageProfile()
    {
        CreateMap<Message, ResponseMessageDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text ?? string.Empty))
            .ForMember(dest => dest.SentimentLabel, opt => opt.MapFrom(src => src.Label))
            .ForMember(dest => dest.SentimentScore, opt => opt.MapFrom(src => src.Score))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
    }
}