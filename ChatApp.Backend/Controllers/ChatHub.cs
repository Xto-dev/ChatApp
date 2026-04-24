using ChatApp.Backend.Infrastructure.DTO;
using ChatApp.Backend.Usecases;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Backend.Controllers;


public class ChatHub(
    IChatHubLog _log,
    IValidator<CreateMessageDto> _createMessageValidator,
    CreateMessageHandler _createMessageHandler
) : Hub
{
    public async Task SendMessage(CreateMessageDto message)
    {
        var validationResult = await _createMessageValidator.ValidateAsync(message);
        if (!validationResult.IsValid)
            return;

        try
        {
            var responseMessage = await _createMessageHandler.HandleAsync(message);
            await Clients.All.SendAsync("ReceiveMessage", responseMessage);
        }
        catch (Exception ex)
        {
            _log.SendMessageError(ex.Message);
        }
    }
}
