namespace ShowerTimer.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using ShowerTimer.Core;

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

            this.Playlist = new List<IActionSequence>()
                            {
                                new ShampooTimeSequence(new TimeSpan(0, 0, 5, 0)),
                                new ConditionerTimeSequence(new TimeSpan(0, 0, 4, 50)),
                                new BodyTimeSequence(new TimeSpan(0, 0, 4, 40)),
                                new FinishTimeSequence(new TimeSpan(0, 0, 4, 30, 0)),
                            };
        }

        public IList<IActionSequence> Playlist { get; }

        public DispatcherTimer Timer { get; }

        public IActionSequence ActivePlaylist { get; private set; }

        private void TimerOnTick(object sender, object o)
        {
            TimeSpan currentTime = TimeSpan.Parse(this.Clock.Text);

            // match action and select in list
            var playlist = this.Playlist.FirstOrDefault(x => x.TargetPlayTime.Equals(currentTime));
            if (playlist != null)
            {
                var item = this.SoundsList.Items.Cast<ListBoxItem>().FirstOrDefault(x => (string)x.Content == playlist.SequenceName);
                if (item != null)
                {
                    this.SoundsList.SelectedIndex = this.SoundsList.Items.IndexOf(item);
                }
            }

            // update clock
            TimeSpan newTime = currentTime.Subtract(this.Timer.Interval);
            this.UpdateClock(newTime);
        }

        private void UpdateClock(TimeSpan newTime)
        {
            this.Clock.Text = string.Format("{0:00}:{1:00}:{2:00}", newTime.Hours, newTime.Minutes, newTime.Seconds);
        }

        private void SoundsListOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // skip unselects?
            if (this.SoundsList.SelectedItem == null) return;

            // run selected
            this.ActivePlaylist = this.Playlist.First(x => x.SequenceName == (string)((ListBoxItem)this.SoundsList.SelectedItem)?.Content);
            this.ActivePlaylist.Run();
            
            // update clock
            this.UpdateClock(this.ActivePlaylist.TargetPlayTime);
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
                    this.ActivePlaylist.Abort();
                    break;
            }
        }
    }
}
