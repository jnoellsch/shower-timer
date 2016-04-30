namespace ShowerTimer.Core
{
    using System;

    public interface IActionSequence
    {
        string SequenceName { get; }

        TimeSpan TargetPlayTime { get; } 

        void Run();

        void Abort();
    }
}
