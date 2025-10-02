namespace Music_Player.Core
{
    /// <summary>
    /// Controlador centralizado de reprodução que coordena AudioPlayer e PlaylistManager
    /// </summary>
    public class PlaybackController
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly PlaylistManager _playlistManager;

        public bool IsShuffle { get; set; }
        public RepeatMode RepeatMode { get; set; }

        public event EventHandler? TrackChanged;
        public event EventHandler? PlaybackStateChanged;

        public PlaybackController(AudioPlayer audioPlayer, PlaylistManager playlistManager)
        {
            _audioPlayer = audioPlayer ?? throw new ArgumentNullException(nameof(audioPlayer));
            _playlistManager = playlistManager ?? throw new ArgumentNullException(nameof(playlistManager));

            _audioPlayer.PlaybackStopped += OnPlaybackStopped!;
        }

        public bool Play()
        {
            if (!_playlistManager.HasSongs)
                return false;

            if (!_audioPlayer.HasAudio)
            {
                if (_playlistManager.CurrentIndex == -1)
                    _playlistManager.SetCurrentIndex(0);

                if (!LoadCurrentTrack())
                    return false;
            }

            _audioPlayer.Play();
            PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void Pause()
        {
            _audioPlayer.Pause();
            PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            _audioPlayer.Stop();
            PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void TogglePlayPause()
        {
            if (_audioPlayer.IsPlaying)
                Pause();
            else
                Play();
        }

        public bool Next()
        {
            if (!_playlistManager.HasSongs)
                return false;

            _playlistManager.MoveToNext(IsShuffle);

            if (LoadCurrentTrack())
            {
                _audioPlayer.Play();
                TrackChanged?.Invoke(this, EventArgs.Empty);
                PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        public bool Previous()
        {
            if (!_playlistManager.HasSongs)
                return false;

            // Se já tocou mais de 3 segundos, reinicia a música atual
            if (_audioPlayer.CurrentTime.TotalSeconds > 3)
            {
                _audioPlayer.CurrentTime = TimeSpan.Zero;
                return true;
            }

            _playlistManager.MoveToPrevious(IsShuffle);

            if (LoadCurrentTrack())
            {
                _audioPlayer.Play();
                TrackChanged?.Invoke(this, EventArgs.Empty);
                PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        public bool LoadTrack(string filePath)
        {
            try
            {
                _playlistManager.SetCurrentFile(filePath);
                return LoadCurrentTrack();
            }
            catch
            {
                return false;
            }
        }

        private bool LoadCurrentTrack()
        {
            try
            {
                string? currentFile = _playlistManager.CurrentFile;
                if (string.IsNullOrEmpty(currentFile))
                    return false;

                _audioPlayer.LoadFile(currentFile);
                TrackChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void OnPlaybackStopped(object sender, EventArgs e)
        {
            switch (RepeatMode)
            {
                case Core.RepeatMode.One:
                    _audioPlayer.CurrentTime = TimeSpan.Zero;
                    _audioPlayer.Play();
                    break;

                case Core.RepeatMode.All:
                    Next();
                    break;

                case Core.RepeatMode.Off:
                    if (_playlistManager.CurrentIndex < _playlistManager.Count - 1)
                        Next();
                    else
                        PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        public AudioPlayer AudioPlayer => _audioPlayer;
        public PlaylistManager PlaylistManager => _playlistManager;
    }

    public enum RepeatMode
    {
        Off = 0,
        All = 1,
        One = 2
    }
}