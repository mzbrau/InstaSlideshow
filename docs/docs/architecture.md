---
id: architecture
title: Architecture
sidebar_label: Architecture
sidebar_position: 4
description: Technical overview of InstaSlideshow's component design, data flow, and key classes.
---

# Architecture

This page describes how InstaSlideshow is structured internally and how its components interact.

---

## High-level overview

```
┌─────────────────────────────────────────────────────┐
│                   MainWindow (WPF)                  │
│                                                     │
│  HeadingText ──────────────────────────────────┐   │
│  InstaImage (cross-fade control)               │   │
│  Username label ───────────────────────────────┘   │
│                    ▲ binds to                       │
│          MainWindowViewModel                        │
│                    │ creates & subscribes           │
│          SlideshowManager                           │
│            ├── Timer (TransitionSpeedMs)            │
│            └── InstaWrapper                         │
│                  └── InstaSharper API               │
└─────────────────────────────────────────────────────┘
```

---

## Component descriptions

### `App.xaml` / `App.xaml.cs`

Standard WPF application entry point. Contains no custom logic — startup is handled by `MainWindow`.

---

### `MainWindow.xaml` / `MainWindow.xaml.cs`

The single WPF window. Key properties:

- **Borderless** (`WindowStyle="None"`) and **maximised** (`WindowState="Maximized"`) to achieve full-screen kiosk mode.
- Transparent background with a black grid so only the content is visible.
- Keyboard (`Escape`) and mouse events are forwarded from `MainWindowViewModel`.
- Uses the `ChangeSource` extension method on the `Image` control to produce a cross-fade animation between photos.

Layout is a three-row `Grid`:

| Row | Content |
|---|---|
| Auto | `HeadingText` (top, centred, 60 pt bold white) |
| `*` | `InstaImage` (fills remaining height) |
| Auto | `Username` (bottom-right, 40 pt bold white) |

---

### `MainWindowViewModel`

MVVM ViewModel that bridges the WPF view and business logic.

**Responsibilities:**
1. Reads settings via `AppSettings` and calls `Validate()`.
2. Creates a `SlideshowManager` and subscribes to `ImageUpdated`.
3. Exposes `Username` and `HeadingText` as bindable properties.
4. On each `ImageUpdated` event, dispatches the new `BitmapImage` and username to the UI.

---

### `SlideshowManager`

Orchestrates the slideshow loop.

**Responsibilities:**
1. Owns a `System.Timers.Timer` set to `TransitionSpeedMs`.
2. Logs in via `InstaWrapper.Login()` on construction; starts the timer only if login succeeds.
3. On each timer tick (`NextAction`):
   - If the local image list is exhausted (or empty), fetches a fresh batch from `InstaWrapper.GetMedia()`.
   - Otherwise, picks the next `InstaImage` from the list, creates a `BitmapImage`, and raises `ImageUpdated`.
4. Stops the timer around each tick to prevent overlapping fetches, then restarts it in the `finally` block.

```
Timer.Elapsed
    │
    ▼
NextAction()
    ├── images empty? ──→ InstaWrapper.GetMedia() → refill list
    └── images available? ──→ pick next → raise ImageUpdated
```

---

### `InstaWrapper`

Thin wrapper around the **InstaSharper** library.

**Key methods:**

| Method | Description |
|---|---|
| `Login()` | Authenticates with Instagram. Loads cached state from `state.bin` first; saves updated state after success. |
| `GetMedia()` | Fetches tag-feed pages for the configured hashtag. Filters by `StartDate`. Flattens both regular posts and carousel items into a flat `List<InstaImage>`. |
| `IsLoggedIn()` | Returns `true` if the underlying API client is authenticated. |

**Session persistence:**  
InstaSharper supports serialising login state to a stream. `InstaWrapper` saves this stream to `state.bin` beside the executable. On subsequent launches the state is reloaded, avoiding a full username/password login.

---

### `AppSettings`

Reads key-value pairs from `ConfigurationManager.AppSettings` using lazy-loaded `Lazy<T>` properties. Each setting is parsed with `TypeDescriptor`, falling back to a default value on parse failure.

---

### `InstaImage`

Simple data-transfer object:

```csharp
public class InstaImage
{
    public string Url  { get; set; }
    public string User { get; set; }
}
```

---

### `NewImageEvent`

`EventArgs` subclass carrying the `BitmapImage` and poster username from `SlideshowManager` to `MainWindowViewModel`.

---

### `ImageExtensions`

Provides the `ChangeSource` extension method on `System.Windows.Controls.Image`. Animates a cross-fade transition by fading the existing image out and fading the new one in using WPF `DoubleAnimation`.

---

## Data flow

```
Instagram API
    │  tag feed (pages)
    ▼
InstaWrapper.GetMedia()
    │  List<InstaImage> (URL + username)
    ▼
SlideshowManager._images[]
    │  picks one per timer tick
    ▼  BitmapImage + username
NewImageEvent
    │  subscribed by
    ▼
MainWindowViewModel.OnImageUpdated()
    │  Dispatcher.Invoke
    ▼
MainWindow → Image.ChangeSource()  →  cross-fade on screen
```

---

## Logging

NLog is configured via `NLog.config` (beside the executable). By default, logs are written to a rolling file and the debug output window. Log messages are emitted at key points:

| Level | When |
|---|---|
| `Info` | Login success/failure, image load count, setting values |
| `Debug` | Each image display (`image N of M, user: …`) |
| `Error` | API errors, setting parse failures, display exceptions |
