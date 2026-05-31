---
id: intro
title: Introduction
sidebar_label: Introduction
sidebar_position: 1
description: An overview of InstaSlideshow — a C# WPF application that turns any screen into a live Instagram hashtag slideshow.
---

# InstaSlideshow

**InstaSlideshow** is a C# / WPF desktop application that displays a live, full-screen slideshow of public Instagram photos tagged with a hashtag of your choice.

It is ideal for events, trade shows, digital signage, or any scenario where you want to show real-time social media activity on a large display.

---

## What it does

- Authenticates with Instagram using your credentials.
- Fetches recent public posts for a configurable hashtag (including carousel albums).
- Displays each image full-screen with smooth cross-fade transitions.
- Automatically refreshes the media pool so the screen never goes stale.
- Shows the poster's name on-screen with each image.

---

## Key features

| Feature | Details |
|---|---|
| **Full-screen WPF window** | Borderless, maximised, always-on-top kiosk mode |
| **Hashtag-driven** | Works with any public hashtag |
| **Carousel support** | Every image in a multi-photo post is shown individually |
| **Configurable transitions** | Set the slide duration in milliseconds |
| **Date filtering** | Show only photos taken after a specific date |
| **Pagination** | Control how many pages of results are fetched per cycle |
| **Logging** | NLog-powered structured logging for diagnostics |
| **Zero-code config** | All settings are in `App.config` — no recompile needed |

---

## Technology stack

| Component | Technology |
|---|---|
| Language | C# (.NET Framework 4.8) |
| UI Framework | Windows Presentation Foundation (WPF) |
| Instagram API | InstaSharper |
| Logging | NLog |
| Installer | Visual Studio Deployment Project (`.vdproj`) |

---

## Next steps

- [Getting Started](./getting-started) — install and configure the application.
- [Configuration Reference](./configuration) — detailed description of every setting.
- [Architecture](./architecture) — learn how the components fit together.
