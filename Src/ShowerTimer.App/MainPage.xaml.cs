namespace ShowerTimer.App
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Windows.Devices.Gpio;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using ShowerTimer.Core;
    using ShowerTimer.Core.Extensions;

    public sealed partial class MainPage : Page
    {
        private const int GpioStartTimerPin = 19;
        private const int GpioProfileSwapPin = 26;
        private GpioPin _startTimerPin;
        private GpioPin _profileSwapPin;

        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeGpio();

            this.Timer = new DispatcherTimer();
            this.Timer.Interval = new TimeSpan(0, 0, 1);
            this.Timer.Tick += this.TimerOnTick;

            this.GpioWatcher = new DispatcherTimer();
            this.GpioWatcher.Interval = TimeSpan.FromMilliseconds(500);
            this.GpioWatcher.Tick += this.GpioWatcherOnTick;
            this.GpioWatcher.Start();

            this.Unloaded += this.MainPageUnloaded;
        }


        public DispatcherTimer GpioWatcher { get; set; }

        public IProfile ActiveProfile { get; private set; }

        public IActionSequence ActiveSequence { get; private set; }

        public DispatcherTimer Timer { get; }

        private void MainPageUnloaded(object sender, RoutedEventArgs e)
        {
            this._startTimerPin?.Dispose();
            this._profileSwapPin?.Dispose();
        }

        private void InitializeGpio()
        {
            GpioController gpio = GpioController.GetDefault();
            if (gpio == null) return;

            this._startTimerPin = gpio.OpenPin(GpioStartTimerPin);
            this._startTimerPin.SetDriveMode(GpioPinDriveMode.Input);
            this._startTimerPin.Write(GpioPinValue.Low);

            this._profileSwapPin = gpio.OpenPin(GpioProfileSwapPin);
            this._profileSwapPin.SetDriveMode(GpioPinDriveMode.Input);
            this._profileSwapPin.Write(GpioPinValue.Low);
        }

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


        private void GpioWatcherOnTick(object sender, object e)
        {
            this.ProcessGpioProfileSwapPin();
            this.ProcessGpioStartPin();
        }

        private void ProcessGpioStartPin()
        {
            var pinVal = this._startTimerPin?.Read();
            if (pinVal == GpioPinValue.High)
            {
                Debug.WriteLine(this._startTimerPin + " = high. START IT!");
                if (!this.Timer.IsEnabled)
                {
                    this.Timer.Start();
                }
            }
        }

        private void ProcessGpioProfileSwapPin()
        {
            var pinVal = this._profileSwapPin?.Read();
            if (pinVal == GpioPinValue.High)
            {
                this.SetBoyProfile();
                Debug.WriteLine(this._profileSwapPin?.PinNumber + " = high. BOY profile.");
            }
            else if (pinVal == GpioPinValue.Low)
            {
                this.SetGirlProfile();
                Debug.WriteLine(this._profileSwapPin?.PinNumber + " = low. GIRL profile.");
            }
        }

        private void UpdateClock(TimeSpan newTime)
        {
            // abort timer if we go negative 
            if (newTime.IsNegative())
            {
                this.PauseActivity();
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
                    this.StartActivity();
                    break;
                case "Pause":
                    this.PauseActivity();
                    break;
            }
        }

        private void PauseActivity()
        {
            this.StartPause.Content = "Start";
            this.Timer.Stop();
            this.ActiveSequence?.Abort();
        }

        private void StartActivity()
        {
            this.StartPause.Content = "Pause";
            this.Timer.Start();
        }

        private void BoyListItemOnTapped(object sender, TappedRoutedEventArgs e)
        {
            this.SetBoyProfile();
        }

        private void GirlListItemOnTapped(object sender, TappedRoutedEventArgs e)
        {
            this.SetGirlProfile();
        }

        private void SetProfile(IProfile profile)
        {
            // pause
            this.PauseActivity();

            // reset profiles
            this.ActiveProfile = profile;
            this.UpdateClock(profile.StartTime);
            this.SequenceList.ItemsSource = profile.Playlist;
        }

        private void SetBoyProfile()
        {
            this.SetProfile(new BoyProfile());
            new SpeechComponent().Speek("Boy profile selected.");
        }

        private void SetGirlProfile()
        {
            this.SetProfile(new GirlProfile());
            new SpeechComponent().Speek("Girl profile selected.");
        }
    }
}
