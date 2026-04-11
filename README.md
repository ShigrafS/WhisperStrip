# 🌬️ WhisperStrip

**Ambient reminders for a focused mind. Lightweight, native, and out of your way.**

[![Build & Release](https://github.com/[USERNAME]/WhisperStrip/actions/workflows/release.yml/badge.svg)](https://github.com/[USERNAME]/WhisperStrip/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)

WhisperStrip is a minimalist Windows utility that gently nudges you with custom reminders. Instead of intrusive popups, it displays subtle text strips on the edges of your screen, helping you stay mindful of your habits, posture, or goals without breaking your flow.

---

## ✨ Features

- **🌬️ Ambient Reminders**: Subtle text strips that appear and vanish naturally.
- **🕒 Dynamic Timing**: Choose between **Exact Intervals** or **Random Ranges** for a more organic feel.
- **📝 Fully Customizable**: Add your own list of mantras, tasks, or reminders.
- **🔄 Smart Rotation**: Cycles through your reminders so they stay fresh.
- **📥 System Tray Native**: Stays out of your taskbar; manage everything from the tray icon.
- **⚡ Lightweight**: Built with .NET 8 for high performance and low resource usage.

---

## 🚀 Getting Started

### Prerequisites

- [Windows 10/11](https://www.microsoft.com/windows)
- [.NET 8.0 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Download the latest release from the [Releases page](https://github.com/[USERNAME]/WhisperStrip/releases).
2. Extract the `WhisperStrip-win-x64.zip`.
3. Run `WhisperStrip.exe`.

---

## 🛠️ Configuration

WhisperStrip saves your preferences in `%LOCALAPPDATA%\WhisperStrip\settings.json`. You can configure:

| Setting | Description |
| :--- | :--- |
| `Texts` | A list of strings to display. |
| `UseRandomTimers` | Toggle between randomized and exact show/hide intervals. |
| `Show/Hide Min/Max` | Control how long strips stay visible and how long they stay hidden. |
| `RotateInterval` | Frequency of rotating to the next message in your list. |

---

## 🏗️ Development

### Built With
- **Language**: C#
- **Framework**: WPF / .NET 8.0
- **Storage**: JSON (System.Text.Json)

### Building from Source
```powershell
# Clone the repository
git clone https://github.com/[USERNAME]/WhisperStrip.git

# Build the project
dotnet build -c Release

# Publish as a single file
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

---

## 📜 License

Distributed under the MIT License. See `LICENSE` for more information.

---

<p align="center">
  <i>Made with ❤️ for productivity.</i>
</p>
