namespace ShowerTimer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.Media.Core;

    public class FinishTimeSequence : IActionSequence
    {
        private AudioComponent _audio;

        public FinishTimeSequence(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public string SequenceName => "Finish";

        public TimeSpan TargetPlayTime { get; }

        private List<MediaSource> MediaSources => new List<MediaSource>()
                                              {
                                                    MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/finalcountdown.mp3")),
                                                    MediaSource.CreateFromUri(new Uri("ms-appx:///Cues/jumparound.mp3"))
                                              };

        private AudioComponent Audio
        {
            get
            {
                return this._audio ?? (this._audio = this.PickRandomAudio());
            }

            set
            {
                this._audio = value;
            }
        }

        public void Run()
        {
            this.Audio = this.PickRandomAudio();
            this.Audio.Play();
        }

        public void Abort()
        {
            this.Audio.Pause();
        }

        private AudioComponent PickRandomAudio()
        {
            Random rnd = new Random();
            return new AudioComponent(this.MediaSources.ElementAt(rnd.Next(this.MediaSources.Count)));
        }
    }
}
