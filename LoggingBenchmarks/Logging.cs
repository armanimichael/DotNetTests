using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace LoggingBenchmarks;

[MemoryDiagnoser]
public partial class Logging
{
    private const string FormatString = "Test {A} {B} {C}";
    private readonly ILogger<Logging> _baseLogger;

    private readonly Action<ILogger, int, int, int, Exception?> _optimizedLogger;
    private readonly Action<ILogger, int, int, int, Exception?> _optimizedLoggerTrace;

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = FormatString)]
    public partial void OptimizedLoggerSrcGen(int a, int b, int c);

    [LoggerMessage(EventId = 1, Level = LogLevel.Trace, Message = FormatString)]
    public partial void OptimizedLoggerSrcGenTrace(int a, int b, int c);

    public Logging()
    {
        _baseLogger = NullLogger<Logging>.Instance;
        _optimizedLogger = LoggerMessage.Define<int, int, int>(
            LogLevel.Information,
            new EventId(1, nameof(Logging)),
            FormatString
        );

        _optimizedLoggerTrace = LoggerMessage.Define<int, int, int>(
            LogLevel.Trace,
            new EventId(1, nameof(Logging)),
            FormatString
        );
    }

    [Benchmark]
    public void SimpleLogging()
    {
        _baseLogger.LogInformation(FormatString, 1, 2, 3);
    }

    [Benchmark]
    public void LoggerMessage_Delegate()
    {
        _optimizedLogger(_baseLogger, 1, 2, 3, null);
    }

    [Benchmark]
    public void LoggerMessage_SrcGen()
    {
        OptimizedLoggerSrcGen(1, 2, 3);
    }

    [Benchmark]
    public void SimpleLogger_Inactive_LogLevel()
    {
        _baseLogger.LogTrace(FormatString, 1, 2, 3);
    }

    [Benchmark]
    public void LoggerMessage_Delegate_Inactive_LogLevel()
    {
        _optimizedLoggerTrace(_baseLogger, 1, 2, 3, null);
    }

    [Benchmark]
    public void LoggerMessage_SrcGen_Inactive_LogLevel()
    {
        OptimizedLoggerSrcGenTrace(1, 2, 3);
    }

    [Benchmark]
    public void SimpleLogger_Inactive_LogLevel_Conditional()
    {
        if (_baseLogger.IsEnabled(LogLevel.Trace))
        {
            _baseLogger.LogTrace(FormatString, 1, 2, 3);
        }
    }

    [Benchmark]
    public void LoggerMessage_Delegate_Inactive_LogLevel_Conditional()
    {
        if (_baseLogger.IsEnabled(LogLevel.Trace))
        {
            _optimizedLoggerTrace(_baseLogger, 1, 2, 3, null);
        }
    }

    [Benchmark]
    public void LoggerMessage_SrcGen_Inactive_LogLevel_Conditional()
    {
        if (_baseLogger.IsEnabled(LogLevel.Trace))
        {
            OptimizedLoggerSrcGenTrace(1, 2, 3);
        }
    }
}