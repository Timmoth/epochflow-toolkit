using Microsoft.Extensions.Logging;

namespace EpochFlow.CpuMetrics.Startup;

public class SpectreConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new SpectreConsoleLogger();
    }

    public void Dispose()
    {

    }
}