namespace ShowerTimer.App
{
    using System;
    using Windows.Media.Core;
    using Windows.UI.Xaml.Controls;

    public class AudioComponent
    {
        public MediaElement Media;

        public AudioComponent(MediaSource mediaSource)
        {
            this.Media = new MediaElement();
            this.Media.AutoPlay = false;
            this.Media.SetPlaybackSource(mediaSource);
        }
        public void Play()
        {
            this.Media.Play();
        }

        public void Stop()
        {
            this.Media.Stop();
        }
    }
}