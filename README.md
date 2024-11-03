# User Navigation Behavior with Different Locomotion Techniques in VR

This repository contains the code and assets for a virtual reality (VR) experimental platform developed to study user navigation behavior using various locomotion techniques. The project was implemented as part of a bachelor’s thesis to investigate how walking, steering, and teleportation techniques influence user interaction and preference in VR environments.

## Project Overview

The aim of this project is to explore user behavior in a VR environment where multiple locomotion techniques are available. Participants are tasked with collecting virtual mushrooms in a forest environment, using any combination of three primary locomotion techniques:
- **Walking**: Physical walking with VR tracking.
- **Steering**: Controller-based movement directed by the user's gaze.
- **Teleportation**: Instant movement to targeted locations.

### Key Features
- **VR Environment**: A virtual forest setting with various obstacles such as trees, rocks, and bushes, designed to mimic a realistic, immersive experience.
- **Multiple Locomotion Techniques**: Allows users to select walking, steering, teleportation, or combinations of these techniques for navigation.
- **Data Collection**: Captures data on user navigation behavior, collision counts, and mushroom collection metrics for analysis.

## Getting Started

### Prerequisites
- Unity 2023 or later
- HTC Vive VR setup (or compatible VR headset with controllers)
- [OpenXR](https://www.khronos.org/openxr/) plugin installed in Unity

### Installation
1. Clone this repository.
2. Open the project in Unity.
3. Ensure that the XR Interaction Toolkit and OpenXR plugins are installed via the Unity Package Manager.
4. Connect your VR setup (HTC Vive recommended) and calibrate the VR environment.

## Usage
1. Load the main scene (MainScene.unity).
2. Start the VR environment by pressing Play in Unity.
3. Follow the instructions displayed in the VR HUD to calibrate your starting position.
4. Choose a locomotion technique to navigate and collect mushrooms within the time limit.

### Controls
1. Walking: Physically move in the real world to navigate.
2. Steering: Use the controller's trigger button to move forward in the direction of your gaze.
3. Teleportation: Aim the controller to select a target location and press the trigger button to teleport.

### Data Collection
All interaction data is stored in a CSV file, including timestamps, mushroom collection counts, and collision counts. This data is used for analyzing user behavior and preferences with different locomotion techniques.

### File Structure
- Assets/ - Unity assets, including scripts, models, and prefabs.
- Scripts/ - All C# scripts for locomotion control, collision handling, data recording, and scene generation.
- Scenes/ - Unity scenes, including the primary environment for the experiment.
- Data/ - Directory for generated CSV files containing experimental data.
