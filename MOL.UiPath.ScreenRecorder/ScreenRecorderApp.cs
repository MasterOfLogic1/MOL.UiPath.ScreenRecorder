using System;
using System.Diagnostics;
using System.IO;

namespace MOL.Uipath.ScreenRecorder
{
    internal static class ScreenRecorderApp
    {
        public static void StartRecorder(string executablePath, string videoOutputFilePath, int screenWidth, int screenHeight,
                bool shouldStopAfterTargetProcessEnds, string targetProcessName, bool shouldStopAfterMaxDuration, float maxDurationInSeconds)
        {
            Console.WriteLine("Setting up recorder");

            if (File.Exists(videoOutputFilePath))
            {
                File.Delete(videoOutputFilePath);
            }

            if(screenWidth <= 0) 
            {
                //default screen width
                screenWidth = 1920;
            }

            if (screenHeight <= 0)
            {
                //default screen height
                screenHeight = 1080;
            }

            if (!shouldStopAfterTargetProcessEnds || string.IsNullOrEmpty(targetProcessName)) 
            {
                shouldStopAfterTargetProcessEnds = false;
                targetProcessName = string.Empty;

                if(!shouldStopAfterMaxDuration || maxDurationInSeconds <= 0)
                {
                    //if no custom maxduration then use default
                    shouldStopAfterMaxDuration = true;
                    maxDurationInSeconds = 30; 
                }
                Console.WriteLine("Screen recorder will start and stop after " + maxDurationInSeconds.ToString() + " Secs");
            }
            else
            {
                Console.WriteLine("Screen recorder will start and stop after " + targetProcessName + " process closes");

            }

            

            // Combine the command line arguments (match the expected order from the command line example)
            string arguments = $"\"{videoOutputFilePath}\" {screenWidth} {screenHeight} {shouldStopAfterTargetProcessEnds.ToString().ToLower()} " +
                               $"\"{targetProcessName}\" {shouldStopAfterMaxDuration.ToString().ToLower()} {maxDurationInSeconds}";

            // Start the process
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = arguments,
                UseShellExecute = false,  // Optional, to avoid opening a new window
                CreateNoWindow = true     // Optional, to suppress a new console window
            };

            try
            {
                Process.Start(startInfo);
                Console.WriteLine("Screen Recorder started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
