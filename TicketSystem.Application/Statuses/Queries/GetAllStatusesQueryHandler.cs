namespace TicketSystem.Application.Statuses.Queries;

using MediatR;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;

public class GetAllStatusesQueryHandler : IRequestHandler<GetAllStatusesQuery, IEnumerable<Status>>
{
    private readonly IStatusRepository _statusRepository;

    public GetAllStatusesQueryHandler(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<IEnumerable<Status>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _statusRepository.GetAllAsync();
    }
}
