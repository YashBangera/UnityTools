# Versioning Guide

This document explains how to properly version and use the Scriptable Object Framework package.

## For Package Users

### Installing Specific Versions

When adding the package to your Unity project, you can specify which version to use:

#### Latest Version (Default)
```json
"com.ybdev.unitytools": "https://github.com/YashBangera/UnityTools.git"
```

#### Specific Version (Recommended for Production)
```json
"com.ybdev.unitytools": "https://github.com/YashBangera/UnityTools.git#0.0.1"
```

#### From a Specific Branch
```json
"com.ybdev.unitytools": "https://github.com/YashBangera/UnityTools.git#develop"
```

### Updating Safely

1. **Check the CHANGELOG**: Always review the [CHANGELOG.md](CHANGELOG.md) before updating to understand what has changed
2. **Test in a Separate Branch**: Create a new git branch in your project before updating
3. **Verify Compatibility**: Ensure the new version supports your Unity version
4. **Run Tests**: Test all systems using the framework after updating

### Version Pinning Best Practices

- **Production Projects**: Always pin to a specific version (e.g., `#0.0.1`)
- **Development Projects**: You may use the latest version but be prepared for potential breaking changes
- **Team Projects**: Ensure all team members use the same version by committing the `packages-lock.json` file

## For Maintainers

### Version Numbering

This project follows [Semantic Versioning](https://semver.org/):

**MAJOR.MINOR.PATCH**

- **MAJOR**: Incompatible API changes (breaking changes)
- **MINOR**: New functionality in a backwards compatible manner
- **PATCH**: Backwards compatible bug fixes

#### Examples:
- `0.0.1` → `0.0.2`: Bug fixes only
- `0.0.1` → `0.1.0`: New features added, existing features unchanged
- `0.0.1` → `1.0.0`: Breaking changes to existing APIs

### Creating a New Release

1. **Update Version Number**
   ```json
   // In package.json
   "version": "0.0.2"
   ```

2. **Update CHANGELOG.md**
   - Add a new section with the version and date
   - List all changes under appropriate categories (Added, Changed, Fixed, etc.)

3. **Commit Changes**
   ```bash
   git add package.json CHANGELOG.md
   git commit -m "chore: bump version to 0.0.2"
   ```

4. **Create Git Tag**
   ```bash
   git tag -a v0.0.2 -m "Version 0.0.2"
   ```

5. **Push to Repository**
   ```bash
   git push origin main
   git push origin v0.0.2
   ```

6. **Create GitHub Release**
   - Go to GitHub repository → Releases → Create new release
   - Select the tag you just created
   - Copy the CHANGELOG content for this version
   - Publish the release

### Breaking Changes Policy

When introducing breaking changes:

1. **Increment MAJOR Version**: Move from 0.x.x to 1.0.0 (or 1.x.x to 2.0.0, etc.)
2. **Document Migration Steps**: Include a migration guide in the CHANGELOG
3. **Deprecation Period**: When possible, deprecate features before removing them
4. **Clear Communication**: Use GitHub release notes to highlight breaking changes

### Pre-release Versions

For testing new features before official release:

```json
"version": "1.0.0-beta.1"
```

Users can install pre-release versions:
```json
"com.ybdev.unitytools": "https://github.com/YashBangera/UnityTools.git#v1.0.0-beta.1"
```

## Version History

- **0.0.1**: Initial release (current)
- Future versions will be documented in [CHANGELOG.md](CHANGELOG.md)

## Troubleshooting

### Package Not Updating
1. Clear the Package Manager cache: `Window > Package Manager > Advanced > Reset Packages to defaults`
2. Delete `Library/PackageCache` folder
3. Reimport the package

### Version Conflicts
- Check `packages-lock.json` for the actual resolved version
- Ensure no other packages depend on a different version
- Use `git diff` to see what changed between versions

### Finding Available Versions
View all available versions on GitHub:
1. Go to the repository
2. Click on "Tags" or "Releases"
3. Each tag represents an available version