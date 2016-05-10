namespace ShowerTimer.Core
{
    using System;

    public class SequenceBase
    {
        protected SequenceBase(TimeSpan targetPlayTime)
        {
            this.TargetPlayTime = targetPlayTime;
        }

        public TimeSpan TargetPlayTime { get; }
    }
}