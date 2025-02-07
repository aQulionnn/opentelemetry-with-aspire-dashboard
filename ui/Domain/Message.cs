namespace ui.Domain;

public class Message
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime SendedOnUtc { get; set; } = DateTime.UtcNow;
}