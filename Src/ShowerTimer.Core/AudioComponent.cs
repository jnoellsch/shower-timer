namespace ShowerTimer.Core
{
    using Windows.Media.Core;
    using Windows.Media.Playback;

    public class AudioComponent
    {
        public MediaPlayer Player;

        public AudioComponent(MediaSource mediaSource)
        {
            this.Player = BackgroundMediaPlayer.Current;
            this.Player.AutoPlay = false;
            this.Player.Source = mediaSource;
        }

        public void Play()
        {
            this.Player.Play();
        }

        public void Pause()
        {
            this.Player.Pause();
        }
    }
}