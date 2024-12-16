using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Desky.ScreenRecorder.Utility
{
    internal static class Helper
    {
        public static void DownloadFileFromGitHubRepo(string url, string outputPath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result; // Blocking call for .NET Framework
                response.EnsureSuccessStatusCode();

                using (Stream contentStream = response.Content.ReadAsStreamAsync().Result) // Get the content as a stream
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Console.WriteLine("Downloading ZIP file from remote URL...");
                    contentStream.CopyTo(fileStream); // Copy content stream to file stream
                    Console.WriteLine("Download complete!");
                }
            }
        }

        public static void ExtractZipFile(string zipFilePath, string destinationFolderPath)
        {
            if (Directory.Exists(destinationFolderPath))
            {
                Directory.Delete(destinationFolderPath, true);
            }
            Console.WriteLine("Extracting ZIP file...");
            ZipFile.ExtractToDirectory(zipFilePath, destinationFolderPath);
        }

        public static void KillProcessByName(string processName)
        {
            try
            {
                Console.WriteLine($"Killing current {processName} process running....");
                // Get all processes by the specified name (without the ".exe" extension)
                Process[] processes = Process.GetProcessesByName(processName.Replace(".exe", ""));

                foreach (var process in processes)
                {
                    // Kill the process
                    process.Kill();
                    Console.WriteLine($"Process {processName} has been terminated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
