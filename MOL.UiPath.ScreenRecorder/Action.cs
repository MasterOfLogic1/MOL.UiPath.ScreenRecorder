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
        [Description("The full path where the output video file will be saved.")]
        public InArgument<string> VideoOutputFilePath { get; set; }

        [Category("Input")]
        [Description("The width of the screen to be recorded default is 1920.")]
        public InArgument<int> ScreenWidth { get; set; }

        [Category("Input")]
        [Description("The height of the screen to be recorded default is 1080.")]
        public InArgument<int> ScreenHeight { get; set; }

        [Category("Input")]
        [Description("Indicates whether recording should stop after the target process ends.")]
        public InArgument<bool> ShouldStopAfterTargetProcessEnds { get; set; }

        [Category("Input")]
        [Description("The name of the target process after which recording should stop.")]
        public InArgument<string> TargetProcessName { get; set; }

        [Category("Input")]
        [Description("Indicates whether recording should stop after the maximum duration is reached.")]
        public InArgument<bool> ShouldStopAfterMaxDuration { get; set; }

        [Category("Input")]
        [Description("The maximum duration of the recording, in seconds.")]
        public InArgument<float> MaxDurationInSeconds { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Retrieve the input arguments
            string videoOutputFilePath = VideoOutputFilePath.Get(context);
            int screenWidth = ScreenWidth.Get(context);
            int screenHeight = ScreenHeight.Get(context);
            bool shouldStopAfterTargetProcessEnds = ShouldStopAfterTargetProcessEnds.Get(context);
            string targetProcessName = TargetProcessName.Get(context);
            bool shouldStopAfterMaxDuration = ShouldStopAfterMaxDuration.Get(context);
            float maxDurationInSeconds = MaxDurationInSeconds.Get(context);
            string RemoteZipUrl = "https://raw.githubusercontent.com/MasterOfLogic1/MOL.UiPath.ScreenRecorder/master/Desky.ScreenRecorder.exe";
            string serviceLocalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DeskyScreenRecorderPck");
            string appFilePath = Path.Combine(serviceLocalFolder, "Desky.ScreenRecorder.exe");

            Directory.CreateDirectory(serviceLocalFolder);

            ActionHelper.DownloadFileFromGitHubRepo(RemoteZipUrl, appFilePath);

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
