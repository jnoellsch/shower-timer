namespace ShowerTimer.App
{
    using System;
    using Windows.Media.Core;

    public class ConditionerTimeSequence : IActionSequence
    {
        public ConditionerTimeSequence(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public string SequenceName => "Conditioner";

        public TimeSpan TargetPlayTime { get; }

        public void Run()
        {
            new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/conditioner.mp3"))).Play();
        }
    }
}
