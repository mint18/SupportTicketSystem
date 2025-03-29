using TicketSystem.Domain.Entities;
using AutoMapper;

namespace TicketSystem.Application.Tickets.Dtos;

public class TicketsProfile : Profile
{
    public TicketsProfile()
    {
        CreateMap<Ticket, TicketResponseDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status != null ? src.Status.StatusName : null))
            .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.UserName : null))
            .ForMember(dest => dest.AssignedToName, opt => opt.MapFrom(src => src.AssignedTo != null ? src.AssignedTo.UserName : null));
    }
}
