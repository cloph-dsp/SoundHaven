# SoundHaven
![Unity Version](https://img.shields.io/badge/Unity-2023.1.20f1-blue.svg)
![License](https://img.shields.io/badge/License-MIT-green.svg)

SoundHaven is an experimental Unity prototype for mobile Virtual Auditory Spaces (VAS), created as part of my master's dissertation to explore interactive spatial audio using Google's Resonance Audio for real-time spatialization.
Read the full dissertation (in Portuguese) [here](https://recil.ulusofona.pt/items/345ce9f3-39ac-4eb1-92c4-3f8d081a7f37).

## Table of Contents
- [Features](#features)
- [Demo](#demo)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features
| Category     | Details                                                               |
|--------------|-----------------------------------------------------------------------|
| Controls     | Touch movement & rotation<br>Gyroscope toggle                         |
| Audio        | Real-time spatialization via ResonanceAudio<br>Quality presets & depth-of-field |
| UI & Input   | Interactive placement & control of sound sources<br>Mobile-optimized input module |

## Demo
[![Watch the Demo](https://img.youtube.com/vi/7A92wuLIImw/0.jpg)](https://youtu.be/7A92wuLIImw)

## Getting Started
### Prerequisites
- Unity 2023.1.20f1
- Android/iOS Build Support for mobile deployment

### Installation
```bash
# Clone the repository
git clone https://github.com/clpoh-dsp/SoundHaven.git
cd SoundHaven

# Open in Unity Hub or Unity Editor
```

### Running the Project
1. Open the project in Unity 2023.1.20f1.
2. Load the scene: `Assets/Scenes/v0.01.unity`.
3. Click **Play** in the Editor or build to your target device.

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
