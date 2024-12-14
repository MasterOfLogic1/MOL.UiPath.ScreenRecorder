using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using Desky.ScreenRecorder.Utility;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Linq;

namespace Desky.ScreenRecorder.Orchestrator
{
    public class ScreenRecorder
    {
        private bool isRecording = false;
        private int frameRate = 30; // Frames per second
        private int frameCount = 0; // Track the current frame
        private System.Threading.Timer frameCaptureTimer;
        private string videoOutputFilePath;
        private readonly List<string> frameFiles = new List<string>();
        private static readonly string RemoteZipUrl = "https://raw.githubusercontent.com/MasterOfLogic1/MOL.UiPath.ScreenRecorder/master/ffmpeg.zip";
        private static readonly string serviceLocalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DeskyScreenRecorderPck");
        private static readonly string localZipFilePath = Path.Combine(serviceLocalFolder, "ffmpeg.zip");
        private static readonly string destinationAppFolderPath = Path.Combine(serviceLocalFolder, "app");
        public static string ffmpegPath = Path.Combine(destinationAppFolderPath, @"bin\ffmpeg.exe");
        public int recorderWidth = 1920;  // Default width
        public int recorderHeight = 1080;

        private ScreenRecorder() { }

        public static ScreenRecorder Initialize(string videoOutputFilePath, int screenWidth = 0, int screenHeight = 0)
        {
            var recorder = new ScreenRecorder
            {
                videoOutputFilePath = videoOutputFilePath
            };

            if (Directory.Exists(Path.Combine(serviceLocalFolder, "Frames")))
            {
                Directory.Delete(Path.Combine(serviceLocalFolder, "Frames"), true);
            }
            recorder.recorderWidth = screenWidth == 0 ? 1920 : screenWidth;
            recorder.recorderHeight = screenHeight == 0 ? 1080 : screenHeight;
            Directory.CreateDirectory(Path.Combine(serviceLocalFolder, "Frames"));
            return recorder;
        }

        public void StartRecording()
        {
            if (isRecording) return;

            isRecording = true;

            // Ensure FFmpeg is downloaded and available
            if (!File.Exists(ffmpegPath))
            {
                Helper.DownloadFileFromGitHubRepo(RemoteZipUrl, localZipFilePath);
                Helper.ExtractZipFile(localZipFilePath, destinationAppFolderPath);
                File.Delete(localZipFilePath);
            }

            Console.WriteLine("Recording started. Press 'S' to stop or 'Q' to quit.");

            frameCaptureTimer = new System.Threading.Timer(CaptureFrame, null, 0, 1000 / frameRate);

            // Wait for user input to stop recording
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.S || key == ConsoleKey.Q)
                {
                    StopRecording();
                    break;
                }
            }
        }

        public void StopRecording()
        {
            if (!isRecording) return;
            Console.WriteLine("Stopping recording...");
            isRecording = false;
            frameCaptureTimer?.Dispose();

            Console.WriteLine("Recording stopped. Generating video...");
            GenerateVideo(Path.Combine(serviceLocalFolder, "Frames"), videoOutputFilePath, frameRate);
        }

        private void CaptureFrame(object state)
        {
            if (!isRecording) return;

            string directory = Path.Combine(serviceLocalFolder, "Frames");
            string frameFile = Path.Combine(directory, $"frame{frameCount++.ToString("D5")}.png");

            // Set custom bounds (x, y, width, height)
            int x = 0;  // X coordinate
            int y = 0;  // Y coordinate
            int width = this.recorderWidth;  // Custom width
            int height = this.recorderHeight;  // Custom height

            Rectangle bounds = new Rectangle(x, y, width, height);

            // Create a Bitmap with the specified size
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                // Capture the defined region of the screen
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }

            // Save the screenshot
            screenshot.Save(frameFile, System.Drawing.Imaging.ImageFormat.Png);
            frameFiles.Add(frameFile);
        }


        private void GenerateVideo(string imagesDirectory, string outputVideoPath, int frameRate)
        {
            if (!Directory.Exists(imagesDirectory))
            {
                Console.WriteLine("The images directory does not exist.");
                return;
            }

            string inputPattern = Path.Combine(imagesDirectory, "frame%05d.png");
            string command = $"-framerate {frameRate} -i \"{inputPattern}\" -vf scale=iw:ih -c:v libx264 -preset veryfast -crf 23 -pix_fmt yuv420p \"{outputVideoPath}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                Process process = Process.Start(startInfo);
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                Console.WriteLine($"Video created successfully at {outputVideoPath}");

                Directory.Delete(imagesDirectory, true); // Clean up frames
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating video: {ex.Message}");
            }
        }


        internal async Task StartRecording(int maxDuration)
        {
            if (isRecording) return;

            isRecording = true;

            // Ensure FFmpeg is downloaded and available
            if (!File.Exists(ffmpegPath))
            {
                Helper.DownloadFileFromGitHubRepo(RemoteZipUrl, localZipFilePath);
                Helper.ExtractZipFile(localZipFilePath, destinationAppFolderPath);
                File.Delete(localZipFilePath);
            }

            frameCaptureTimer = new System.Threading.Timer(CaptureFrame, null, 0, 1000 / frameRate);

            // Track the start time
            DateTime startTime = DateTime.Now;

            // Wait for user input or until maxDuration is exceeded
            while (true)
            {
                // Check if the max duration has been exceeded
                if ((DateTime.Now - startTime).TotalSeconds >= maxDuration)
                {
                    StopRecording();
                    break;
                }

                // Optionally, add a small delay to avoid excessive CPU usage in the loop
                await Task.Delay(100); // Adjust this delay as needed
            }
        }

        internal async Task StartRecording(string targetProcessName)
        {
            if (isRecording) return;

            isRecording = true;

            // Ensure FFmpeg is downloaded and available
            if (!File.Exists(ffmpegPath))
            {
                Helper.DownloadFileFromGitHubRepo(RemoteZipUrl, localZipFilePath);
                Helper.ExtractZipFile(localZipFilePath, destinationAppFolderPath);
                File.Delete(localZipFilePath);
            }

            frameCaptureTimer = new System.Threading.Timer(CaptureFrame, null, 0, 1000 / frameRate);

            // Loop to continuously check if the app is running
            while (true)
            {
                var processes = Process.GetProcessesByName(targetProcessName);

                // If no processes are found, the app is not running
                if (!processes.Any())
                {
                    Console.WriteLine($"{targetProcessName} is no longer running.");
                    StopRecording();
                    break;  // Stop the loop if the app is no longer running
                }
                // Wait for a while before checking again (e.g., 5 seconds)
                await Task.Delay(5000);
            }
        }

    }
}
