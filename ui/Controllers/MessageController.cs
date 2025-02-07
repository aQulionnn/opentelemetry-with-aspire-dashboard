using Microsoft.AspNetCore.Mvc;
using ui.Domain;

namespace ui.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<MessageController> _logger;

    public MessageController(AppDbContext context, ILogger<MessageController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Send([FromBody] string messageDescription)
    {
        var message = _context.Messages.Add(new Message{Description = messageDescription});
        _context.SaveChanges();
        
        DignosticsConfig.SendMessagesCounter.Add(
            1, 
            new KeyValuePair<string, object?>("messages.description", messageDescription),
            new KeyValuePair<string, object?>("message.send", message.Entity.SendedOnUtc.Date.ToShortDateString()));
        
        _logger.LogInformation($"Message sent: {message.Entity.Id}");
        
        return Ok(message.Entity);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Messages.ToList());
    }
}