using ChatApp.Backend.Infrastructure.DTO;
using FluentValidation;

namespace ChatApp.Backend.Infrastructure;
public class CreateMessageValidator : AbstractValidator<CreateMessageDto>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
    }
}