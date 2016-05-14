namespace ShowerTimer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class BoyProfile : IProfile
    {
        public BoyProfile()
        {
            this.Playlist = new List<IActionSequence>()
                            {
                                new IntroTimeSequence(new TimeSpan(0, 0, 3, 0)),
                                new ShampooTimeSequence(new TimeSpan(0, 0, 2, 30)),
                                new BodyTimeSequence(new TimeSpan(0, 0, 1, 30)),
                                new FinishTimeSequence(new TimeSpan(0, 0, 0, 0))
                            }; 
        }

        public string ProfileName => "Boy";

        public IList<IActionSequence> Playlist { get; }

        public TimeSpan StartTime => this.Playlist.First().TargetPlayTime;
    }
}
