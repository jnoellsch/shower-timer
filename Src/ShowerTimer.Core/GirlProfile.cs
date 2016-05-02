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
                                new IntroTimeSequence(new TimeSpan(0, 0, 5, 45)),
                                new ShampooTimeSequence(new TimeSpan(0, 0, 4, 15)),
                                new ConditionerTimeSequence(new TimeSpan(0, 0, 2, 15)),
                                new BodyTimeSequence(new TimeSpan(0, 0, 0, 45)),
                                new FinishTimeSequence(new TimeSpan(0, 0, 0, 0))
                            };
        }

        public IList<IActionSequence> Playlist { get; }

        public TimeSpan StartTime => this.Playlist.First().TargetPlayTime;
    }
}
