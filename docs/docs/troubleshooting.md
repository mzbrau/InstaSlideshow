---
id: troubleshooting
title: Troubleshooting
sidebar_label: Troubleshooting
sidebar_position: 5
description: Solutions to common InstaSlideshow issues including login failures, blank screens, and configuration errors.
---

# Troubleshooting

This page covers the most common problems encountered when running InstaSlideshow and how to resolve them.

---

## Checking the log file

InstaSlideshow uses **NLog** for diagnostic logging. The log file is located beside the executable:

```
C:\Program Files\InstaSlideshow\logs\InstaSlideshow.log
```

Check this file first whenever something goes wrong — it almost always contains the root cause.

---

## Common issues

### "Invalid Settings" dialog on startup

**Symptom:** A dialog appears saying *"Invalid application settings. Modify the app settings file to include valid username, password and hashtag."*

**Cause:** One or more of `Username`, `Password`, or `Hashtag` is blank in `App.config`.

**Fix:**

1. Open `InstaSlideshow.exe.config` in a text editor.
2. Ensure all three keys have non-empty values:

   ```xml
   <add key="Hashtag"  value="myeventhashtag"/>
   <add key="Username" value="instagram_user"/>
   <add key="Password" value="instagram_pass"/>
   ```

3. Save and restart the application.

---

### Login fails / "Unable to login" in the log

**Symptom:** Log shows `Unable to login: ...` and no images appear.

**Possible causes and fixes:**

| Cause | Fix |
|---|---|
| Wrong username or password | Double-check the values in `App.config`. |
| Two-Factor Authentication (2FA) enabled | Disable 2FA on the Instagram account or use an account without 2FA. |
| Instagram suspicious activity checkpoint | Log in to Instagram via a web browser from the same machine to clear the checkpoint, then retry. |
| Stale `state.bin` session file | Delete `state.bin` from the application directory to force a fresh login. |
| Instagram API rate limiting | Wait a few minutes before retrying, or reduce `PagesCount`. |

---

### Blank / black screen after login

**Symptom:** The application starts and the heading is visible, but no images are displayed.

**Possible causes and fixes:**

| Cause | Fix |
|---|---|
| Hashtag has no recent posts | Try a popular hashtag to verify the app works, then investigate the custom hashtag. |
| All posts are older than `StartDate` | Set `StartDate` to an earlier date, or remove it (set to `01/01/2000`). |
| `PagesCount` is 0 | Set `PagesCount` to at least `1`. |
| Network connectivity issue | Verify internet access; check corporate proxy settings. |

---

### Window does not appear full-screen / covers only part of the screen

**Symptom:** The window appears but does not cover the taskbar or is not maximised.

**Fix:** Ensure the application is launched as **Administrator**:

1. Right-click `InstaSlideshow.exe`.
2. Select **Run as administrator**.

Alternatively, set the compatibility option permanently:

1. Right-click `InstaSlideshow.exe` → **Properties**.
2. Go to the **Compatibility** tab.
3. Check **Run this program as an administrator**.

---

### Images load slowly or the slideshow stutters

**Symptom:** There is a noticeable delay before the next image appears, or the transition is choppy.

**Possible causes and fixes:**

| Cause | Fix |
|---|---|
| High `PagesCount` | Reduce `PagesCount` to `2` or `3` to shorten fetch time. |
| Slow internet connection | Nothing application-specific; improve network conditions. |
| Large images | Instagram serves images at native resolution; this is expected on slower connections. |
| `TransitionSpeedMs` too low | Increase to at least `5000` to give images time to load before the next transition. |

---

### `state.bin` errors in the log

**Symptom:** Log contains errors about loading state from file.

**Fix:** Delete `state.bin` from the application directory. The file may be corrupted or from an incompatible version of InstaSharper.

---

### Application crashes on startup with a missing DLL error

**Symptom:** A Windows error dialog reports a missing `.dll`.

**Fix:** Install the .NET Framework 4.6.1 runtime from the [Microsoft Download Center](https://dotnet.microsoft.com/download/dotnet-framework/net461).

---

## Still stuck?

If none of the above resolves your issue:

1. Enable verbose logging in `NLog.config` (set the minimum log level to `Trace`).
2. Reproduce the issue and capture the full log.
3. [Open a GitHub issue](https://github.com/mzbrau/InstaSlideshow/issues/new) and attach the log file.
