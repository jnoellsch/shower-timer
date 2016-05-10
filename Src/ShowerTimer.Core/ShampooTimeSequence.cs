namespace ShowerTimer.Core
{
    using System;
    using Windows.Media.Core;

    public class ShampooTimeSequence : SequenceBase, IActionSequence
    {

        public ShampooTimeSequence(TimeSpan targetPlayTime)
            : base(targetPlayTime)
        {
        }

        public string SequenceName => "Shampoo";

        private AudioComponent Audio => new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/shampoo.mp3")));

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
