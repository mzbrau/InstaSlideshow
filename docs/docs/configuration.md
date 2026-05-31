---
id: configuration
title: Configuration Reference
sidebar_label: Configuration
sidebar_position: 3
description: Full reference for all InstaSlideshow App.config settings including valid values, defaults, and examples.
---

# Configuration Reference

All InstaSlideshow settings are stored in the application configuration file (`App.config` / `InstaSlideshow.exe.config`). The file uses the standard .NET `<appSettings>` format — no code changes are needed.

---

## Settings file location

| Scenario | File path |
|---|---|
| Installer build | `C:\Program Files\InstaSlideshow\InstaSlideshow.exe.config` |
| Source / Debug build | `InstaSlideshow\bin\Debug\InstaSlideshow.exe.config` |
| Source / Release build | `InstaSlideshow\bin\Release\InstaSlideshow.exe.config` |

:::tip
You can also edit `InstaSlideshow\App.config` in the source tree before building so that the output config is pre-populated.
:::

---

## Settings reference

### HeadingText

```xml
<add key="HeadingText" value="#MyEvent"/>
```

| Property | Value |
|---|---|
| Type | `string` |
| Default | `"Heading"` |
| Required | No |

The large text displayed at the **top** of the screen. Typically set to a branded event name or the hashtag you are displaying, e.g. `#TechConf2025`.

---

### Hashtag

```xml
<add key="Hashtag" value="TechConf2025"/>
```

| Property | Value |
|---|---|
| Type | `string` |
| Default | `"dog"` |
| Required | **Yes** |

The Instagram hashtag to search for. Enter the tag **without** the `#` prefix. The application will fail validation and refuse to start if this value is empty.

---

### Username

```xml
<add key="Username" value="your_instagram_username"/>
```

| Property | Value |
|---|---|
| Type | `string` |
| Default | *(none)* |
| Required | **Yes** |

The Instagram account username used to authenticate with the API. The application will fail validation and refuse to start if this value is empty.

:::caution Security
Credentials are stored in plain text. Keep the config file permissions tight and never commit it to source control.
:::

---

### Password

```xml
<add key="Password" value="your_password"/>
```

| Property | Value |
|---|---|
| Type | `string` |
| Default | *(none)* |
| Required | **Yes** |

The password for the Instagram account above. Subject to the same security caution as `Username`.

---

### TransitionSpeedMs

```xml
<add key="TransitionSpeedMs" value="8000"/>
```

| Property | Value |
|---|---|
| Type | `int` (milliseconds) |
| Default | `5000` |
| Required | No |
| Min recommended | `3000` |

How long each image is displayed on screen before transitioning to the next. The value is in **milliseconds**. Note that the cross-fade animation itself takes approximately 2 seconds (1 s fade-out + 1 s fade-in) and runs within this window.

**Examples:**

| Value | Display time |
|---|---|
| `3000` | 3 seconds |
| `8000` | 8 seconds (default in config file) |
| `15000` | 15 seconds |

---

### PagesCount

```xml
<add key="PagesCount" value="5"/>
```

| Property | Value |
|---|---|
| Type | `int` |
| Default | `5` |
| Required | No |
| Range | 1 – 10+ |

The number of Instagram API result pages to fetch in each media refresh cycle. Each page typically contains 12–20 posts. Higher values show more variety but take longer to load and increase API usage.

---

### StartDate

```xml
<add key="StartDate" value="01/01/2025"/>
```

| Property | Value |
|---|---|
| Type | `DateTime` (`MM/DD/YYYY`) |
| Default | `DateTime.MinValue` (no filter) |
| Required | No |

Posts taken **before** this date are excluded from the slideshow. Set this to the start of your event to avoid unrelated historical content appearing on screen.

---

## Complete example

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
  </startup>
  <appSettings>
    <add key="HeadingText"       value="#TechConf2025"/>
    <add key="Hashtag"           value="TechConf2025"/>
    <add key="Username"          value="your_instagram_username"/>
    <add key="Password"          value="your_instagram_password"/>
    <add key="TransitionSpeedMs" value="10000"/>
    <add key="PagesCount"        value="5"/>
    <add key="StartDate"         value="06/01/2025"/>
  </appSettings>
</configuration>
```

---

## Validation rules

On startup, `AppSettings.Validate()` checks that **Username**, **Password**, and **Hashtag** are all non-empty. If any of these are missing, a dialog is shown and the application exits.
