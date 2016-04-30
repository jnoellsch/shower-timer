namespace ShowerTimer.Core
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

        private AudioComponent Audio => new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/conditioner.mp3")));

        public void Run()
        {
            this.Audio.Play();
        }

        public void Abort()
        {
            this.Audio.Pause();
        }
    }
}
