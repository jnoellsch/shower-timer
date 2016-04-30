namespace ShowerTimer.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.Media.Playback;
    using Windows.Media.SpeechSynthesis;

    public class SpeechComponent
    {
        public async void Speek(string phrase)
        {
            var synthesizer = new SpeechSynthesizer();
            var speechStream = await synthesizer.SynthesizeTextToStreamAsync(phrase);

            var player = BackgroundMediaPlayer.Current;
            player.AutoPlay = true;
            player.SetStreamSource(speechStream);
        }
    }
}