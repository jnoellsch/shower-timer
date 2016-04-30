using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShowerTimer.App
{
    using System;
    using Windows.Media.Playback;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IList<IActionSequence> _actions;

        private DispatcherTimer _timer;

        public MainPage()
        {
            this.InitializeComponent();

            this._timer = new DispatcherTimer();
            this._timer.Interval = new TimeSpan(0, 0, 1);
            this._timer.Tick += this.Timer_OnTick;

            this._actions = new List<IActionSequence>()
                            {
                                new ShampooTimeSequence(new TimeSpan(0, 0, 5, 0)),
                                new ConditionerTimeSequence(new TimeSpan(0, 0, 4, 50)),
                                new BodyTimeSequence(new TimeSpan(0, 0, 4, 40)),
                                new FinishTimeSequence(new TimeSpan(0, 0, 4, 30, 0)),
                            };
        }

        private void Timer_OnTick(object sender, object o)
        {
            TimeSpan currentTime = TimeSpan.Parse(this.Clock.Text);

            // match action, run if applicable
            var toPlay = this._actions.FirstOrDefault(x => x.TargetPlayTime.Equals(currentTime));
            toPlay?.Run();

            // update clock
            TimeSpan newTime = currentTime.Subtract(this._timer.Interval);
            this.Clock.Text = string.Format("{0}:{1}:{2}", newTime.Hours, newTime.Minutes, newTime.Seconds);
        }

        private void Sounds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // run selected
            var toPlay = this._actions.First(x => x.SequenceName.Equals((string)e.AddedItems.Cast<ListBoxItem>().First().Content, StringComparison.OrdinalIgnoreCase));
            toPlay.Run();
        }

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            this._timer.Start();
        }
    }
}
