namespace ShowerTimer.Core
{
    using System;
    using System.Collections.Generic;

    public interface IProfile
    {
        IList<IActionSequence> Playlist { get; }

        TimeSpan StartTime { get; }
    }
}
