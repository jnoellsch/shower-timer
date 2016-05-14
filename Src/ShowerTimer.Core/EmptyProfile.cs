namespace ShowerTimer.Core
{
    using System;
    using System.Collections.Generic;

    public class EmptyProfile : IProfile
    {
        public string ProfileName { get; } = "Empty";

        public IList<IActionSequence> Playlist { get; } = new List<IActionSequence>();

        public TimeSpan StartTime { get; } = new TimeSpan(0, 0, 0);
    }
}
