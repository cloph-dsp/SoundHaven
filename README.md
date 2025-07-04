# SoundHaven
![Unity Version](https://img.shields.io/badge/Unity-2023.1.20f1-blue.svg)
![License](https://img.shields.io/badge/License-MIT-green.svg)

SoundHaven is an experimental Unity prototype for mobile Virtual Auditory Spaces (VAS), created as part of my master's dissertation to explore interactive spatial audio.

## Table of Contents
- [Features](#features)
- [Demo](#demo)
- [Getting Started](#getting-started)
- [Controls](#controls)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)
- [Credits](#credits)

## Features
- Touch-based movement & rotation
- Interactive UI for placing and controlling sound sources
- Audio quality presets and depth-of-field effects
- Gyroscope toggle for device orientation controls
- Real-time audio spatialization using ResonanceAudio plugin
- Custom input module optimized for mobile devices

## Demo
[![Watch the Demo](https://img.youtube.com/vi/7A92wuLIImw/0.jpg)](https://youtu.be/7A92wuLIImw)

## Getting Started
### Prerequisites
- Unity 2023.1.20f1 (see `ProjectSettings/ProjectVersion.txt`)
- Android/iOS Build Support for mobile deployment

### Installation
```bash
# Clone the repository
git clone https://github.com/<username>/SoundHaven.git
cd SoundHaven

# Open in Unity Hub or Unity Editor
```

### Running the Project
1. Open the project in Unity 2023.1.20f1.
2. Load the scene: `Assets/Scenes/v0.01.unity`.
3. Click **Play** in the Editor or build to your target device.

## Controls
### Mobile (Touch Input)
- Left half of the screen: Move character.
- Right half of the screen: Rotate camera.

### UI Buttons
- **Play/Pause**: Toggle audio playback.
- **Gyro**: Enable/disable gyroscope controls.
- **Settings**: Open settings panel to adjust audio quality (Low/High), toggle depth-of-field, and enable/disable gyroscope input.

## Project Structure
```
Assets/
├── Scenes/          # Unity Scenes
├── Scripts/         # C# Scripts
├── UI/              # UI Prefabs and Controllers
└── ...              # Other assets (Plugins, Packages)
ProjectSettings/     # Unity project settings files
Packages/            # Unity package manifest files
```

## Contributing
Contributions are welcome! Please fork this repository and submit a pull request.

## License
This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

## Credits
- **Developer**: kastru
- **Audio Plugin**: ResonanceAudio
