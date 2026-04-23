using ChatApp.Backend.Infrastructure.DTO;
using ChatApp.Backend.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Backend.Controllers;

[ApiController]
[Route("api/messages")]
public class MessagesController (
    GetRecentMessagesHandler _getRecentMessagesHandler
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRecent([FromQuery] int count = 10)
    {
        var messages = await _getRecentMessagesHandler.HandleAsync(count);
        return Ok(messages);
    }
}
