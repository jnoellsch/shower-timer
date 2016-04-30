namespace ShowerTimer.App
{
    using System;

    interface IActionSequence
    {
        string SequenceName { get; }

        TimeSpan TargetPlayTime { get; } 

        void Run();
    }
}
