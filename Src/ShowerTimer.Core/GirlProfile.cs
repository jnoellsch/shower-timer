namespace ShowerTimer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GirlProfile : IProfile
    {
        public GirlProfile()
        {
            this.Playlist = new List<IActionSequence>()
                            {
                                new IntroTimeSequence(new TimeSpan(0, 0, 7, 0)),
                                new ShampooTimeSequence(new TimeSpan(0, 0, 6, 0)),
                                new ConditionerTimeSequence(new TimeSpan(0, 0, 4, 0)),
                                new BodyTimeSequence(new TimeSpan(0, 0, 1, 30)),
                                new FinishTimeSequence(new TimeSpan(0, 0, 0, 0))
                            };
        }

        public IList<IActionSequence> Playlist { get; }

        public TimeSpan StartTime => this.Playlist.First().TargetPlayTime;
    }
}
