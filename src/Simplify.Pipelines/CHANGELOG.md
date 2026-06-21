# Changelog

## [0.9.4] - 2026-06-19

### Removed

- Long-obsolete `Pipeline<T>`, `ResultingPipeline<T,TResult>`, `PipelineProcessor<T>`, `ValidationPipeline<T,TResult>`, `ValidationPipelineProcessor<T,TResult>` and their supporting types (interfaces, rules, data preparers) — use `IConveyor` / `IAsyncConveyor` instead

## [0.9.3] - 2026-05-26

### Fixed

- AsyncConveyor switched to real async instead of .GetAwaiter().GetResult()


## [0.9.2] - 2023-08-01

### Removed

- .NET 4.5.2 support

### Added

- .NET 6 support

## [0.9.1] - 2022-05-02

### Deprecated

- Pipeline, ResultingPipeline and related in favor of Conveyors
- ChainHandler

### Removed

- .NET Standard 1.0 support
