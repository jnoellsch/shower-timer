namespace ShowerTimer.App
{
    using System;
    using Windows.Media.Core;

    public class ShampooTimeSequence : IActionSequence
    {
        public ShampooTimeSequence(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public string SequenceName => "Shampoo";

        public TimeSpan TargetPlayTime { get; }

        public void Run()
        {
            var audio = new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/shampoo.mp3")));
            audio.Play();
        }
    }
}
