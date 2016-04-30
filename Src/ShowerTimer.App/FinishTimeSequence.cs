namespace ShowerTimer.App
{
    using System;
    using Windows.Media.Core;

    public class FinishTimeSequence : IActionSequence
    {
        public FinishTimeSequence(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public string SequenceName => "Finish";

        public TimeSpan TargetPlayTime { get; }

        public void Run()
        {
            new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/jumparound.mp3"))).Play();
        }
    }
}
