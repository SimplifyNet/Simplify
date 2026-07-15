# Changelog

## [1.7.2] - 2026-07-15

### Fixed

- `TransactGenericRepository` rollback on zombie transactions no longer masks the original exception; rollback failures (e.g. lost connection) are now silently swallowed, preserving the original error

## [1.7.1] - 2026-06-25

### Added

- .NET 10 support

### Fixed

- `CommonEqualityComparer.GetHashCode` was inconsistent with `Equals` (it returned the reference-based hash code while `Equals` compared by `ID`), so equal entities ended up in different buckets in hash-based collections (`Dictionary`, `HashSet`, `Distinct`, `GroupBy`); the hash code is now derived from `ID`, and `null` is handled
- `TransactGenericRepository` did not roll back the transaction when a repository operation threw, leaving an open transaction on the connection; all operations now roll back on failure

## [1.7.0] - 2024-01-14

### Changed

- GetMultipleByQuery and GetMultipleByQueryAsync renamed to GetMultiple and GetMultipleAsync

## [1.6.1] - 2023-08-01

### Removed

- .NET Standard 1.2 support
- .NET 4.5.2 support

### Added

- .NET 6 support
- .NET Standard 2.1 support
- .NET 4.8 support

### Dependencies

- Explicit System.Data.Common reference removed

## [1.6.0] - 2021-10-28

### Added

- Object with string identifier
