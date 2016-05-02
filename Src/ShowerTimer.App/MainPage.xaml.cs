namespace ShowerTimer.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using ShowerTimer.Core;
    using ShowerTimer.Core.Extensions;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Timer = new DispatcherTimer();
            this.Timer.Interval = new TimeSpan(0, 0, 1);
            this.Timer.Tick += this.TimerOnTick;
        }

        public IProfile ActiveProfile { get; private set; }
        public IActionSequence ActiveSequence { get; private set; }

        public DispatcherTimer Timer { get; }

        private void TimerOnTick(object sender, object o)
        {
            TimeSpan currentTime = TimeSpan.Parse(this.Clock.Text);

            // match action and select in list
            var playlist = this.ActiveProfile.Playlist.FirstOrDefault(x => x.TargetPlayTime.Equals(currentTime));
            if (playlist != null)
            {
                var item = this.SequenceList.Items.Cast<IActionSequence>().FirstOrDefault(x => x.SequenceName == playlist.SequenceName);
                if (item != null)
                {
                    this.SequenceList.SelectedIndex = this.SequenceList.Items.IndexOf(item);
                }
            }

            // update clock
            TimeSpan newTime = currentTime.Subtract(this.Timer.Interval);
            this.UpdateClock(newTime);
        }

        private void UpdateClock(TimeSpan newTime)
        {
            // abort timer if we go negative 
            if (newTime.IsNegative())
            {
                this.Timer.Stop();
                return;
            }

            // refresh clock
            this.Clock.Text = string.Format("{0:00}:{1:00}:{2:00}", newTime.Hours, newTime.Minutes, newTime.Seconds);
        }

        private void SequenceListOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // skip unselects?
            if (this.SequenceList.SelectedItem == null) return;

            // run selected
            this.ActiveSequence = this.ActiveProfile.Playlist.First(x => x.SequenceName == ((IActionSequence)this.SequenceList.SelectedItem).SequenceName);
            this.ActiveSequence.Run();

            // update clock
            this.UpdateClock(this.ActiveSequence.TargetPlayTime);
        }

        private void StartPauseOnClick(object sender, RoutedEventArgs e)
        {
            switch ((string)this.StartPause.Content)
            {
                case "Start":
                    this.StartPause.Content = "Pause";
                    this.Timer.Start();
                    break;
                case "Pause":
                    this.StartPause.Content = "Start";
                    this.Timer.Stop();
                    break;
            }
        }

        private void ProfileListOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var boy = new BoyProfile();
            var girl = new GirlProfile();

            // pause current activity
            this.StartPause.Content = "Start";
            this.ActiveSequence?.Abort();
            this.Timer.Stop();

            // reset profiles, sequences
            switch ((string)((ListBoxItem)this.ProfileList.SelectedItem)?.Content)
            {
                case "Boy":
                    this.UpdateClock(boy.StartTime);
                    this.ActiveProfile = boy;
                    this.SequenceList.ItemsSource = boy.Playlist;
                    break;
                case "Girl":
                    this.UpdateClock(girl.StartTime);
                    this.ActiveProfile = girl;
                    this.SequenceList.ItemsSource = girl.Playlist;
                    break;
            }
        }
    }
}
