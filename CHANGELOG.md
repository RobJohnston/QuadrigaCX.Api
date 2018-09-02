# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) and this project adheres to Semantic Versioning.

## [Unreleased]

## [0.1.0] - 2018-09-02

### Changed
- Suffix buy and sell methods with Async.
- Update MarketOrder class after experimentation.

### Removed
- Remove GetBooks and GetCurrencies methods, as they are not part of the API.

## [0.0.4] - 2018-03-25
### Added
- Throw an exception if the API returns an error.
- Add `SignatureException` to catch when the client ID, API key, or API secret is invalid.
- Fix to throw `QuadrigaException` when an API call that is supposed to return a string return an error instead.

### Changed
- Create distinct deposit and withdraw methods to replace the generic methods.

### Removed
- Remove the error property added in previous version.

## [0.0.3] - 2018-03-18
### Added
- Add the trading, deposit, and withdrawal functionality (not tested at all).
- Add methods to get currencies and books (these are not part of the API).

### Changed
- Return the error as a property of the object being retrieved instead of throwing an exception.
- Rename methods to make it clear that they are asyncronous.

## [0.0.2] - 2018-03-14
### Changed
- The returned Task is no longer wrapped in a QuadrigaResponse object.

## 0.0.1 - 2018-03-12
### Added
- First alpha release.

[Unreleased]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.1.0-beta...HEAD
[0.1.0]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.4-alpha...v0.1.0-beta
[0.0.4]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.3-alpha...v0.0.4-alpha
[0.0.3]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.2-alpha...v0.0.3-alpha
[0.0.2]: https://github.com/RobJohnston/QuadrigaCX.Api/compare/v0.0.1-alpha...v0.0.2-alpha
