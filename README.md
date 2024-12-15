# MOL.Uipath.ScreenRecorder

## Overview
The **MOL.Uipath.ScreenRecorder** library offers a seamless way to record the screen of a target machine directly from your UiPath workflows. This library is specifically designed to simplify screen recording tasks, making it an excellent tool for process automation and user monitoring scenarios.

---

## Features
- Automatically sets up and starts a screen recorder application.
- Records the screen and saves the output to a user-specified file path.
- Configurable settings for:
  - Screen dimensions (width and height).
  - Automatic stopping of the recording when the target process ends.
  - Maximum recording duration.
- Designed for efficient and user-friendly integration within UiPath workflows.

---

## Compatibility
This library is compatible  with **UiPath Windows & Windows-Legacy projects** and requires the target machine to support the .NET Framework.

### Framework Requirements
- **.NET Framework 4.8**

### UiPath Compatibility
- **Windows-Legacy**: Supported
- **Windows (Modern)**:  Supported

---

## Installation
1. Download the `MOL.Uipath.ScreenRecorder` package from the [Nuget Org](https://www.nuget.org/packages/MOL.Uipath.ScreenRecorder) or nuget on your Uipath Pacage manger.
   ![image](https://github.com/user-attachments/assets/21044106-43eb-41da-be7e-1275b69061ab)

3. Import the package into your UiPath project:
   - Open your UiPath project.
   - Go to the **Manage Packages** menu.
   - Search for `MOL.Uipath.ScreenRecorder` and install it.

---

## How to Use

### Input Properties
- **ScreenWidth** *(Integer)*: The width of the screen to record default is 1920.
- **ScreenHeight** *(Integer)*: The height of the screen to record - default 1080.
- **ShouldStopAfterTargetProcessEnds** *(Boolean)*: Whether the recording should stop when the target process ends- default is false.
- **TargetProcessName** *(String)*: The name of the target process to monitor.
- **ShouldStopAfterMaxDuration** *(Boolean)*: Whether the recording should stop after the maximum duration is reached - default is true.
- **MaxDurationInSeconds** *(Float)*: The maximum duration of the recording in seconds- default is 30 secs.

### Output
- **VideoOutputFilePath** *(String)*: File path where the recording will be saved.

### Example Usage
1. Drag and drop the **Start Screen Recorder** activity into your UiPath workflow.
2. Configure the input properties:
   - Specify the file path for saving the output video.
   - Set the screen dimensions and duration parameters as needed.
3. Run the workflow and the recorder starts seperately while your process runs.

---

## Known Limitations
- The library currently supports only Windows-Legacy projects in UiPath.
- Compatibility with .NET Core and UiPath Windows (Modern) is not available.

---

## Contributors
- **David Oku**  
- **Chinyere Isiekwu**

---

## License
This library is licensed under the MIT License. Refer to the LICENSE file for more details.

---

## Support
For issues or feature requests, please contact:
- Email: david.oku1t@outlook.com
- GitHub: [MOL.Uipath.ScreenRecorder Issues](https://github.com/MasterOfLogic/MOL.Uipath.ScreenRecorder/issues)

