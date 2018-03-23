# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Unreleased]
### Added
- Throw an exception if the API returns an error.

### Removed
- Remove the error property added in previous version.

## [0.0.3] - 2018-03-18
### Added
- Added the trading, deposit, and withdrawal functionality (not tested at all).

### Changed
- Return the error as a property of the object being retrieved instead of throwing an exception.

## [0.0.2] - 2018-03-14
### Changed
- The returned Task is no longer wrapped in a QuadrigaResponse object.

## 0.0.1 - 2018-03-12
### Added
- First alpha release.

[Unreleased]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.3-alpha...HEAD
[0.0.3]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.2-alpha...v0.0.3-alpha
[0.0.2]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.1-alpha...v0.0.2-alpha
