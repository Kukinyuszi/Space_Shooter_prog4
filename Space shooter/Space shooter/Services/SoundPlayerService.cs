using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Space_shooter.Services
{
    public class SoundPlayerService 
    {
        MediaPlayer _backgroundMusic = new MediaPlayer();
        MediaPlayer playershotAudio;
        MediaPlayer enemyshotAudio;

        private bool playing;

        public bool Playing { get => playing; set => playing = value; }

        public SoundPlayerService()
        {
            PlayerShotAudioSetup();
            EnemyShotAudioSetup();
        }

        public MediaPlayer StartBackgroundMusic()
        {
            var cd = Directory.GetCurrentDirectory();
            _backgroundMusic.Open(new Uri(cd + "/Gamemusic.wav"));
            _backgroundMusic.MediaEnded += new EventHandler(BackgroundMusic_Ended);
            _backgroundMusic.Volume = 0.3;
            _backgroundMusic.Play();
            return _backgroundMusic;
        }

        private void BackgroundMusic_Ended(object sender, EventArgs e)
        {
            _backgroundMusic.Position = TimeSpan.Zero;
            _backgroundMusic.Play();
        }


        private void Mute(object sender, EventArgs e)
        {
            _backgroundMusic.Pause();
        }

        private void UnMute(object sender, EventArgs e)
        {
            _backgroundMusic.Play();
        }

        private void VolumeUp(object sender, EventArgs e)
        {
            _backgroundMusic.Volume += 0.1;
        }

        private void VolumeDown(object sender, EventArgs e)
        {
            _backgroundMusic.Volume -= 0.1;
        }

        private void PlayerShotAudioSetup()
        {
            playershotAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            playershotAudio.Open(new Uri(cd + "/Shoot3.wav"));
            playershotAudio.Volume = 0.3;
            playershotAudio.MediaEnded += PlayershotAudio_MediaEnded;
        }
        public void PlayershotAudio_Start(object sender, EventArgs e)
        {
            playershotAudio.Play();
        }


        private void PlayershotAudio_MediaEnded(object sender, EventArgs e)
        {
            playershotAudio.Stop();
        }

        private void EnemyShotAudioSetup()
        {
            enemyshotAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            enemyshotAudio.Open(new Uri(cd + "/Shoot1.wav"));
            enemyshotAudio.Volume = 0.3;
            enemyshotAudio.MediaEnded += EnemyshotAudio_MediaEnded;
        }
        public void EnemyshotAudio_Start(object sender, EventArgs e)
        {
            enemyshotAudio.Play();
        }

        private void EnemyshotAudio_MediaEnded(object sender, EventArgs e)
        {
            enemyshotAudio.Stop();
        }
    }
}
