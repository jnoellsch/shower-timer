namespace ShowerTimer.Core
{
    using System;
    using System.Collections.Generic;

    public interface IProfile
    {
        string ProfileName { get; }

        IList<IActionSequence> Playlist { get; }

        TimeSpan StartTime { get; }
    }
}
