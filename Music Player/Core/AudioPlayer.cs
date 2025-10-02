using NAudio.Wave;

namespace Music_Player.Core
{
    /// <summary>
    /// Classe responsável pela reprodução de áudio usando NAudio
    /// </summary>
    public class AudioPlayer : IDisposable
    {
        private WaveOutEvent? _outputDevice;
        private AudioFileReader? _audioFile;
        private bool _isPaused;

        // Eventos
        public event EventHandler? PlaybackStopped;
        public event EventHandler? PlaybackStateChanged;

        // Propriedades públicas
        public bool IsPlaying => _outputDevice?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => _isPaused;
        public bool HasAudio => _audioFile != null;

        public TimeSpan CurrentTime
        {
            get => _audioFile?.CurrentTime ?? TimeSpan.Zero;
            set
            {
                if (_audioFile != null)
                    _audioFile.CurrentTime = value;
            }
        }

        public TimeSpan TotalTime => _audioFile?.TotalTime ?? TimeSpan.Zero;

        public float Volume
        {
            get => _audioFile?.Volume ?? 0f;
            set
            {
                if (_audioFile != null)
                    _audioFile.Volume = Math.Clamp(value, 0f, 1f);
            }
        }

        /// <summary>
        /// Carrega um arquivo de áudio para reprodução
        /// </summary>
        public void LoadFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Arquivo não encontrado", filePath);

            // Para a reprodução atual
            Stop();

            // Libera recursos anteriores
            _audioFile?.Dispose();
            _outputDevice?.Dispose();

            // Carrega novo arquivo
            _audioFile = new AudioFileReader(filePath);
            _outputDevice = new WaveOutEvent();
            _outputDevice.Init(_audioFile);
            _outputDevice.PlaybackStopped += OnPlaybackStopped!;

            _isPaused = false;
        }

        /// <summary>
        /// Inicia ou retoma a reprodução
        /// </summary>
        public void Play()
        {
            if (_outputDevice == null)
                throw new InvalidOperationException("Nenhum arquivo carregado");

            _outputDevice.Play();
            _isPaused = false;
            PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Pausa a reprodução
        /// </summary>
        public void Pause()
        {
            if (_outputDevice?.PlaybackState == PlaybackState.Playing)
            {
                _outputDevice.Pause();
                _isPaused = true;
                PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Para a reprodução e volta ao início
        /// </summary>
        public void Stop()
        {
            if (_outputDevice != null)
            {
                _outputDevice.Stop();
                _isPaused = false;
                if (_audioFile != null)
                    _audioFile.Position = 0;
                PlaybackStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Alterna entre Play e Pause
        /// </summary>
        public void TogglePlayPause()
        {
            if (IsPlaying)
                Pause();
            else
                Play();
        }

        /// <summary>
        /// Move para uma posição específica do áudio
        /// </summary>
        public void Seek(TimeSpan time)
        {
            if (_audioFile != null)
            {
                _audioFile.CurrentTime = time;
            }
        }

        /// <summary>
        /// Move relativamente (avançar/retroceder)
        /// </summary>
        public void SeekRelative(TimeSpan delta)
        {
            if (_audioFile != null)
            {
                var newTime = _audioFile.CurrentTime + delta;
                _audioFile.CurrentTime = TimeSpan.FromSeconds(
                    Math.Clamp(newTime.TotalSeconds, 0, _audioFile.TotalTime.TotalSeconds));
            }
        }

        /// <summary>
        /// Evento interno de quando a reprodução para
        /// </summary>
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _isPaused = false;
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Libera todos os recursos
        /// </summary>
        void IDisposable.Dispose()
        {
            Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();
        }
    }
}