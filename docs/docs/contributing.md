---
id: contributing
title: Contributing
sidebar_label: Contributing
sidebar_position: 6
description: Guidelines for contributing code, documentation, or bug reports to InstaSlideshow.
---

# Contributing

Contributions to InstaSlideshow are welcome! Whether you are fixing a bug, improving the documentation, or adding a new feature, this guide explains how to get involved.

---

## Code of conduct

Please be respectful and constructive in all interactions. This project follows the [Contributor Covenant](https://www.contributor-covenant.org/version/2/1/code_of_conduct/) code of conduct.

---

## Getting started

### 1. Fork and clone

```bash
# Fork the repository on GitHub, then:
git clone https://github.com/<your-username>/InstaSlideshow.git
cd InstaSlideshow
```

### 2. Create a branch

Use a descriptive branch name:

```bash
git checkout -b feature/add-dark-mode
# or
git checkout -b fix/login-state-corruption
```

### 3. Open in Visual Studio

Open `InstaSlideshow.sln` in **Visual Studio 2019** or later and restore NuGet packages.

### 4. Make your changes

Follow the conventions described below, then build and test locally.

### 5. Submit a pull request

Push your branch to your fork and open a pull request against `main`. Describe what you changed and why.

---

## Development conventions

### Code style

- Follow existing C# naming and formatting conventions present in the codebase.
- Use `var` where the type is obvious from the right-hand side.
- Keep methods short and focused on a single responsibility.
- Add XML doc comments to `public` members.

### MVVM pattern

The UI follows the **Model-View-ViewModel** pattern. Avoid putting business logic in code-behind files (`*.xaml.cs`). Route new features through the ViewModel.

### Logging

Use the existing NLog `ILogger` instance (`_logger`) for diagnostic output. Follow the existing level conventions:

| Level | Use for |
|---|---|
| `Info` | Normal lifecycle events (login, image load) |
| `Debug` | Per-image detail |
| `Error` | Recoverable errors and exceptions |

### Configuration

New settings should be added to `AppSettings` using the existing `Lazy<T>` pattern and exposed as properties. Always provide a sensible default value.

---

## Reporting bugs

1. Check the [existing issues](https://github.com/mzbrau/InstaSlideshow/issues) to avoid duplicates.
2. If not already reported, open a [new issue](https://github.com/mzbrau/InstaSlideshow/issues/new).
3. Include:
   - A clear description of the problem.
   - Steps to reproduce.
   - Expected vs actual behaviour.
   - The relevant section of the NLog log file.
   - Your OS version and .NET Framework version.

---

## Suggesting features

Open a [GitHub issue](https://github.com/mzbrau/InstaSlideshow/issues/new) with the `enhancement` label. Describe the use case and why the feature would benefit others.

---

## Documentation

Documentation lives in the `docs/docs/` directory and is built with [Docusaurus](https://docusaurus.io/). To run the docs site locally:

```bash
cd docs
npm install
npm start
```

The site is served at `http://localhost:3000`. Changes to Markdown files hot-reload automatically.

When adding or editing documentation:
- Include Docusaurus-compatible frontmatter at the top of every `.md` file.
- Keep prose concise and prefer tables and code blocks for reference material.
- Update `sidebars.js` if you add a new page.

---

## Releasing

Releases are managed by the repository maintainer. The installer project (`InstaSlideshowInstall`) produces an `.msi` that is attached to each GitHub Release.
