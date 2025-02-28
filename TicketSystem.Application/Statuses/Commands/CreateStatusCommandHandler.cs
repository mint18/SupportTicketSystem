using MediatR;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;

namespace TicketSystem.Application.Statuses.Commands;

public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, int>
{
    private readonly IStatusRepository _statusRepository;

    public CreateStatusCommandHandler(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<int> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
    {
        var status = new Status { StatusName = request.StatusName };
        var createdStatus = await _statusRepository.AddAsync(status);
        return createdStatus.Id;
    }
}
