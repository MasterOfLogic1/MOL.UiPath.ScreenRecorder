using System;
using System.Activities;
using System.ComponentModel;
using System.IO;
using Desky.Datatable;
using MOL.Uipath.ScreenRecorder;

namespace MOL.UiPath.ScreenRecorder
{
    [DisplayName("Screen Recorder")]
    [Description("Starts up a Screen Recorder that runs on a seperate thread")]
    [Category("Screen Recorder Activities")]
    public class ScreenRecorder : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        [Description("The directory where the output video file will be saved.")]
        public InArgument<string> OutputFolderPath { get; set; }

        [Category("Input")]
        [Description("The desired name of ouput video file without its extension default is current datetimestamp in ddMMyyyyHHmmss")]
        public InArgument<string> OutputVideoFileNameWithoutExtension { get; set; }

        [Category("Input")]
        [Description("The width of the screen to be recorded default is 1920.")]
        public InArgument<int> ScreenWidth { get; set; }

        [Category("Input")]
        [Description("The height of the screen to be recorded default is 1080.")]
        public InArgument<int> ScreenHeight { get; set; }

        [Category("Input")]
        [Description("Indicates whether recording should stop after a specified process ends.")]
        public InArgument<bool> ShouldStopAfterTargetProcessEnds { get; set; }

        [Category("Input")]
        [Description("The name of the specified process after which recording should stop.")]
        public InArgument<string> TargetProcessName { get; set; }

        [Category("Input")]
        [Description("Indicates whether recording should stop after the maximum duration is reached.")]
        public InArgument<bool> ShouldStopAfterMaxDuration { get; set; }

        [Category("Input")]
        [Description("The maximum duration of the recording, in seconds.")]
        public InArgument<float> MaxDurationInSeconds { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            //Retrieve the input arguments
            string outputFolderPath = OutputFolderPath.Get(context);
            string outputVideoFileNameWithoutExtension = OutputVideoFileNameWithoutExtension.Get(context);
            int screenWidth = ScreenWidth.Get(context);
            int screenHeight = ScreenHeight.Get(context);
            bool shouldStopAfterTargetProcessEnds = ShouldStopAfterTargetProcessEnds.Get(context);
            string targetProcessName = TargetProcessName.Get(context);
            bool shouldStopAfterMaxDuration = ShouldStopAfterMaxDuration.Get(context);
            float maxDurationInSeconds = MaxDurationInSeconds.Get(context);
            string RemoteZipUrl = "https://raw.githubusercontent.com/MasterOfLogic1/MOL.UiPath.ScreenRecorder/master/Desky.ScreenRecorder.exe";
            string serviceLocalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DeskyScreenRecorderApp");
            string appFilePath = Path.Combine(serviceLocalFolder, "Desky.ScreenRecorder.exe");

            if (string.IsNullOrEmpty(outputVideoFileNameWithoutExtension)) 
            {
                outputVideoFileNameWithoutExtension = DateTime.Now.ToString("ddMMyyyyHHmmss");
            }

            if (!Directory.Exists(outputFolderPath)) 
            {
                throw new Exception(outputFolderPath + " does not exists");
            }

            string videoOutputFilePath = Path.Combine(outputFolderPath, outputVideoFileNameWithoutExtension + ".mp4");

            Directory.CreateDirectory(serviceLocalFolder);

            ActionHelper.DownloadFileFromGitHubRepo(RemoteZipUrl, appFilePath);

            //kill process
            ActionHelper.KillProcessByName("Desky.ScreenRecorder.exe");
            //Call the existing StartRecorder function
            ScreenRecorderApp.StartRecorder(
                appFilePath,
                videoOutputFilePath,
                screenWidth,
                screenHeight,
                shouldStopAfterTargetProcessEnds,
                targetProcessName,
                shouldStopAfterMaxDuration,
                maxDurationInSeconds
                );
        }

    }

}
