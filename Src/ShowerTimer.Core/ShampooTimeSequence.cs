﻿namespace ShowerTimer.Core
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

        private AudioComponent Audio => new AudioComponent(MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/shampoo.mp3")));

        private SpeechComponent Speech => new SpeechComponent();

        public void Run()
        {
            //this.Speech.Speek("Put shampoo on your head.");
            this.Audio.Play();
        }

        public void Abort()
        {
            this.Audio.Pause();
        }
    }
}
