﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;


namespace Desky.Datatable
{
    internal static class ActionHelper
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
                    Console.WriteLine("Downloading file from remote URL...");
                    contentStream.CopyTo(fileStream); // Copy content stream to file stream
                    Console.WriteLine("Download complete!");
                }
            }
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
