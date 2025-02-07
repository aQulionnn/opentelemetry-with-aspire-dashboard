using System.Diagnostics.Metrics;

namespace ui;

public static class DignosticsConfig
{
    public const string ServiceName = "WebApi";
    
    public static Meter Meter = new (ServiceName);
    
    public static Counter<int> SendMessagesCounter = Meter.CreateCounter<int>("SendMessages");
}