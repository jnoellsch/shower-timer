namespace ShowerTimer.Core
{
    using System;

    public class EmptySequence : IActionSequence
    {
        public string SequenceName { get; } = "Empty";

        public TimeSpan TargetPlayTime { get; } = new TimeSpan(0, 0, 0);

        public void Run()
        {
        }

        public void Abort()
        {
        }
    }
}
