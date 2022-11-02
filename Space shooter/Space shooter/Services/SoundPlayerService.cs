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
        MediaPlayer gameMusicAudio;
        MediaPlayer playerShotAudio;
        MediaPlayer enemyShotAudio;
        MediaPlayer coinAudio;
        MediaPlayer healthAudio;
        MediaPlayer powerupAudio;
        MediaPlayer shieldAudio;
        MediaPlayer explosionAudio;

        private bool playing;

        public bool Playing { get => playing; set => playing = value; }

        public SoundPlayerService()
        {
            PlayerShotAudioSetup();
            EnemyShotAudioSetup();
            CoinAudioSetup();
            HealthAudioSetup();
            PowerupAudioSetup();
            ShieldAudioSetup();
            ExplosionAudioSetup();
        }

        public MediaPlayer StartBackgroundMusic()
        {
            gameMusicAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            gameMusicAudio.Open(new Uri(cd + "/Gamemusic.wav"));
            gameMusicAudio.MediaEnded += BackgroundMusic_Ended;
            gameMusicAudio.Volume = 0.3;
            gameMusicAudio.Play();
            return gameMusicAudio;
        }

        private void BackgroundMusic_Ended(object sender, EventArgs e)
        {
            gameMusicAudio.Position = TimeSpan.Zero;
            gameMusicAudio.Play();
        }


        public void Mute(object sender, EventArgs e)
        {
            gameMusicAudio.Pause();
        }

        public void UnMute(object sender, EventArgs e)
        {
            gameMusicAudio.Play();
        }

        public void VolumeUp(object sender, EventArgs e)
        {
            gameMusicAudio.Volume += 0.1;
        }

        public void VolumeDown(object sender, EventArgs e)
        {
            gameMusicAudio.Volume -= 0.1;
        }

        private void PlayerShotAudioSetup()
        {
            playerShotAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            playerShotAudio.Open(new Uri(cd + "/Shoot3.wav"));
            playerShotAudio.Volume = 0.3;
            playerShotAudio.MediaEnded += PlayershotAudio_MediaEnded;
        }
        public void PlayershotAudio_Start(object sender, EventArgs e)
        {
            playerShotAudio.Stop();
            playerShotAudio.Play();
        }

        private void PlayershotAudio_MediaEnded(object sender, EventArgs e)
        {
            playerShotAudio.Stop();
        }

        private void EnemyShotAudioSetup()
        {
            enemyShotAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            enemyShotAudio.Open(new Uri(cd + "/Shoot1.wav"));
            enemyShotAudio.Volume = 0.3;
            enemyShotAudio.MediaEnded += EnemyshotAudio_MediaEnded;
        }
        public void EnemyshotAudio_Start(object sender, EventArgs e)
        {
            enemyShotAudio.Stop();
            enemyShotAudio.Play();
        }

        private void EnemyshotAudio_MediaEnded(object sender, EventArgs e)
        {
            enemyShotAudio.Stop();
        }
        private void CoinAudioSetup()
        {
            coinAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            coinAudio.Open(new Uri(cd + "/PowerupCoin.wav"));
            coinAudio.Volume = 0.3;
            coinAudio.MediaEnded += CoinAudio_MediaEnded;
        }
        public void CoinAudio_Start(object sender, EventArgs e)
        {
            coinAudio.Stop();
            coinAudio.Play();
        }

        private void CoinAudio_MediaEnded(object sender, EventArgs e)
        {
            coinAudio.Stop();
        }
        private void HealthAudioSetup()
        {
            healthAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            healthAudio.Open(new Uri(cd + "/PowerupHealth.wav"));
            healthAudio.Volume = 0.3;
            healthAudio.MediaEnded += HealthAudio_MediaEnded;
        }
        public void HealthAudio_Start(object sender, EventArgs e)
        {
            healthAudio.Stop();
            healthAudio.Play();
        }

        private void HealthAudio_MediaEnded(object sender, EventArgs e)
        {
            healthAudio.Stop();
        }
        private void PowerupAudioSetup()
        {
            powerupAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            powerupAudio.Open(new Uri(cd + "/Powerup.wav"));
            powerupAudio.Volume = 0.3;
            powerupAudio.MediaEnded += PowerupAudio_MediaEnded;
        }
        public void PowerupAudio_Start(object sender, EventArgs e)
        {
            powerupAudio.Stop();
            powerupAudio.Play();
        }

        private void PowerupAudio_MediaEnded(object sender, EventArgs e)
        {
            powerupAudio.Stop();
        }
        private void ExplosionAudioSetup()
        {
            explosionAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            explosionAudio.Open(new Uri(cd + "/Exploding.wav"));
            explosionAudio.Volume = 0.3;
            explosionAudio.MediaEnded += ExplosionAudio_MediaEnded;
        }
        public void ExplosionAudio_Start(object sender, EventArgs e)
        {
            explosionAudio.Stop();
            explosionAudio.Play();
        }

        private void ExplosionAudio_MediaEnded(object sender, EventArgs e)
        {
            explosionAudio.Stop();
        }
        private void ShieldAudioSetup()
        {
            shieldAudio = new MediaPlayer();
            var cd = Directory.GetCurrentDirectory();
            shieldAudio.Open(new Uri(cd + "/PowerupShield.wav"));
            shieldAudio.Volume = 0.3;
            shieldAudio.MediaEnded += ShieldAudio_MediaEnded;
        }
        public void ShieldAudio_Start(object sender, EventArgs e)
        {
            shieldAudio.Stop();
            shieldAudio.Play();
        }

        private void ShieldAudio_MediaEnded(object sender, EventArgs e)
        {
            shieldAudio.Stop();
        }
    }
}
