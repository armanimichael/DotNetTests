``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 7 3800XT, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.103
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  DefaultJob : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT


```
|                                               Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
|----------------------------------------------------- |----------:|----------:|----------:|-------:|----------:|
|                                        SimpleLogging | 57.991 ns | 0.3172 ns | 0.2967 ns | 0.0143 |     120 B |
|                               LoggerMessage_Delegate |  3.253 ns | 0.0272 ns | 0.0255 ns |      - |         - |
|                                 LoggerMessage_SrcGen |  1.761 ns | 0.0139 ns | 0.0123 ns |      - |         - |
|                       SimpleLogger_Inactive_LogLevel | 56.365 ns | 0.4400 ns | 0.4116 ns | 0.0143 |     120 B |
|             LoggerMessage_Delegate_Inactive_LogLevel |  3.231 ns | 0.0248 ns | 0.0232 ns |      - |         - |
|               LoggerMessage_SrcGen_Inactive_LogLevel |  1.769 ns | 0.0074 ns | 0.0069 ns |      - |         - |
|           SimpleLogger_Inactive_LogLevel_Conditional |  1.594 ns | 0.0169 ns | 0.0158 ns |      - |         - |
| LoggerMessage_Delegate_Inactive_LogLevel_Conditional |  1.995 ns | 0.0147 ns | 0.0137 ns |      - |         - |
|   LoggerMessage_SrcGen_Inactive_LogLevel_Conditional |  1.563 ns | 0.0197 ns | 0.0184 ns |      - |         - |
