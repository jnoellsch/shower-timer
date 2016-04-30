namespace ShowerTimer.Core
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

        private AudioComponent Audio => new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/body.mp3")));

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
