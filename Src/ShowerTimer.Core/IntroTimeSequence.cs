namespace ShowerTimer.Core
{
    using System;

    public class IntroTimeSequence : IActionSequence
    {
        public IntroTimeSequence(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public string SequenceName => "Intro";

        public TimeSpan TargetPlayTime { get; }

        private SpeechComponent Speech => new SpeechComponent();

        public void Run()
        {
            this.Speech.Speek("Turn on the water and get in the shower.");
        }

        public void Abort()
        {
            // nop
        }
    }
}