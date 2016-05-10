namespace ShowerTimer.Core
{
    using System;

    public class IntroTimeSequence : SequenceBase, IActionSequence
    {
        public IntroTimeSequence(TimeSpan targetPlayTime) : base(targetPlayTime)
        {
        }

        public string SequenceName => "Intro";

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