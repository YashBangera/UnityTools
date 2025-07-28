# Changelog

All notable changes to the Scriptable Object Framework will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.2] - 2024-01-28

### Added
- Comprehensive XML documentation for all Value ScriptableObjects
- XML documentation for BaseValueSO class and all its methods
- Improved code documentation for better IntelliSense support

### Changed
- Updated package.json version to 0.0.2

### Fixed
- Event System documentation in README to accurately reflect actual implementation
- Corrected CustomArgs usage examples showing proper GetArg<T>() method
- Added comprehensive EventListener component documentation

## [0.0.1] - 2024-01-28

### Added
- Initial release of Scriptable Object Framework
- **Value ScriptableObjects**: Int, Float, Bool, String, Vector3, GameObject, and Texture2D value containers
- **Collection ScriptableObjects**: Dynamic runtime collections for all value types
- **Event System**: Decoupled event communication with EventSO, EventListener, and CustomArgs
- **Reference System**: Transform reference sharing across scenes and systems
- **Localization System**: Simple localization using LocalizedString ScriptableObjects
- **Editor Tools**:
  - Custom inspectors for runtime value monitoring and editing
  - ScriptableObject Reference Finder tool
  - Localization Editor Window
  - Creation wizards for localized UI components
- **Documentation**: Comprehensive README with examples and best practices

### Known Issues
- Collections use O(n) removal complexity - consider using dictionaries for large datasets
- Localization system currently supports basic text localization only

### Notes
- This is the initial public release
- Projects already using this version should continue to work without any changes
- Future versions will follow semantic versioning to ensure compatibility