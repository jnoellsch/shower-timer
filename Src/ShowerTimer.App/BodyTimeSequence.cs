namespace ShowerTimer.App
{
    using System;
    using Windows.Media.Core;

    public class BodyTimeSequence : IActionSequence
    {
        public BodyTimeSequence(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public string SequenceName => "Body";

        public TimeSpan TargetPlayTime { get; }

        public void Run()
        {
            new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/body.mp3"))).Play();
        }
    }
}
