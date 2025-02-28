using TicketSystem.Domain.Entities;
using AutoMapper;

namespace TicketSystem.Application.Tickets.Dtos;

public class TicketsProfile : Profile
{
    public TicketsProfile()
    {
        CreateMap<Ticket, TicketResponseDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status != null ? src.Status.StatusName : null));
    }
}
