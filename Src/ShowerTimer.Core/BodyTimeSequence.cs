namespace ShowerTimer.Core
{
    using System;
    using Windows.Media.Core;

    public class BodyTimeSequence : SequenceBase, IActionSequence
    {

        public BodyTimeSequence(TimeSpan targetPlayTime)
            : base(targetPlayTime)
        {
        }

        public string SequenceName => "Body";

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
