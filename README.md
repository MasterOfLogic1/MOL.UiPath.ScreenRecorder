# MOL.Uipath.ScreenRecorder

## Overview
This UiPath library provides an activity that effortlessly sets up and initiates a screen recording application on the target machine. It captures the screen and saves the recorded output to a specified location. By running the recording on a separate thread, it ensures that other processes are not disrupted. This activity is perfect for situations where you want to monitor and record server activity while your bot performs its tasks, without interrupting ongoing operations.

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
- **OutputFolderPath** *(String)*: Folder path where the recording will be saved.
- **OutputVideoFilenameWithoutExtension** *(String)*: The desired name of ouput video file without its extension default is current datetimestamp in ddMMyyyyHHmms.
- **ScreenWidth** *(Integer)*: The width of the screen to record default is 1920.
- **ScreenHeight** *(Integer)*: The height of the screen to record - default 1080.
- **ShouldStopAfterTargetProcessEnds** *(Boolean)*: Whether the recording should stop when the target process ends- default is false.
- **TargetProcessName** *(String)*: The name of the target process to monitor.
- **ShouldStopAfterMaxDuration** *(Boolean)*: Whether the recording should stop after the maximum duration is reached - default is true.
- **MaxDurationInSeconds** *(Float)*: The maximum duration of the recording in seconds- default is 30 secs.

  ![image](https://github.com/user-attachments/assets/a9c1d9b7-29b6-4402-a1ba-2ffc4cbf6de1)


  ![image](https://github.com/user-attachments/assets/26c84fbe-c98d-4d39-b438-3d2fc04b2772)


### Output


### Example Usage
1. Drag and drop the **Start Screen Recorder** activity into your UiPath workflow.
2. Configure the input properties:
   - Specify the file path for saving the output video.
   - Set the screen dimensions and duration parameters as needed.
3. Run the workflow and the recorder starts seperately while your process runs.

---
### More
Desky.ScreenRecorder is a console application compiled into a .exe file that works with the FFmpeg library. Everything you need to know about FFmpeg can be found here.

When the Desky.ScreenRecorder console application is launched for the first time on any machine, it downloads all the dependencies needed to run the recorder, including FFmpeg. The application starts recording based on instructions provided via command-line arguments.

MOL.Uipath.ScreenRecorder is the corresponding UiPath library. It contains an activity that invokes Desky.ScreenRecorder, passing command-line arguments to the console application as specified in the activity's properties. When the activity is used, the console application runs on a new thread.

---
### Dpendecy
System.Activities: Provides support for creating workflows and activities in .NET.

FFmpeg Library(downloaded or initialized automatically if not present):  The FFmpeg is used for capturing and encoding screen recordings into the output video format (e.g., `.mp4`).
Screen Recorder Console Application (downloaded or initialized automatically if not present):The application relies on a bundled screen recorder executable for managing the recording logic.

.NET Framework/Runtime: Ensure the required .NET version (e.g., .NET Framework 4.6.5 or .NET Core) is installed on the machine running the application.

Target Machine must not have internet restrictions of accessing from github

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
- Email: david.oku1@outlook.com

