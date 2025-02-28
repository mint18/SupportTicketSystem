using FluentValidation;
using TicketSystem.Application.Statuses.Dtos;

namespace TicketSystem.Application.Statuses.Validators;

public class CreateStatusDtoValidator : AbstractValidator<CreateStatusDto>
{
    public CreateStatusDtoValidator()
    {
        RuleFor(x => x.StatusName)
            .NotEmpty().WithMessage("StatusName is required.")
            .MaximumLength(20).WithMessage("StatusName cannot exceed 20 characters.");
    }
}
