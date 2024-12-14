using System;
using System.Threading.Tasks;
using Desky.ScreenRecorder.Orchestrator;

class Program
{
    static async Task Main(string[] args)
    {

        // Check if the arguments are passed
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ScreenRecorder <output_video_path> [screen_width] [screen_height] [should_stop_after_target_process_ends] [should_stop_after_max_duration] [max_duration] [target_process_name]");
            throw new Exception("Please provide <output_video_path> in args as a location to save video output");
        }

        // Get video output file path from the first argument
        string videoOutputFilePath = args[0];

        // Default values for screen width and height
        int screenWidth = 1920;
        int screenHeight = 1080;

        bool shouldStopAfterTargetProcessEnds = false;
        bool shouldStopAfterMaxDuration = false;
        int maxDuration = 0;
        string targetProcessName = "notepad";

        // Check if screen width and height are passed, and parse them
        if (args.Length > 1 && !int.TryParse(args[1], out screenWidth))
        {
            Console.WriteLine("Invalid screen width. Using default value of 1920.");
        }

        if (args.Length > 2 && !int.TryParse(args[2], out screenHeight))
        {
            Console.WriteLine("Invalid screen height. Using default value of 1080.");
        }

        //string videoOutputFilePath = @"C:\Users\david\Videos\AnyDesk\test.mp4";

        // Parse shouldStopAfterTargetProcessEnds (true/false)
        if (args.Length > 3 && !bool.TryParse(args[3], out shouldStopAfterTargetProcessEnds))
        {
            Console.WriteLine("Invalid value for shouldStopAfterTargetProcessEnds. Using default value of false.");
        }

        if (!shouldStopAfterTargetProcessEnds)
        {
            // Parse shouldStopAfterMaxDuration (true/false)
            if (args.Length > 4 && !bool.TryParse(args[4], out shouldStopAfterMaxDuration))
            {
                Console.WriteLine("Invalid value for shouldStopAfterMaxDuration. Using default value of false.");
            }

            // Parse maxDuration (int)
            if (args.Length > 5 && !int.TryParse(args[5], out maxDuration))
            {
                Console.WriteLine("Invalid value for maxDuration. Using default value of 0.");
            }

        }

        ScreenRecorder recorder = ScreenRecorder.Initialize(videoOutputFilePath, screenWidth, screenHeight);
        
        // Start recording
        if (shouldStopAfterMaxDuration)
        {
            await recorder.StartRecording(maxDuration);
            return;
        }

        // Start recording
        if (shouldStopAfterTargetProcessEnds)
        {
            await recorder.StartRecording(targetProcessName);
            return;
        }

        recorder.StartRecording();
    }
}
