using BenchmarkDotNet.Running;
using LoggingBenchmarks;

var summary = BenchmarkRunner.Run<Logging>();