using Music_Player.Core;
using MetadataReader = Music_Player.Core.MetadataReader;

namespace Music_Player
{
    public partial class Form1 : Form
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly PlaylistManager _playlistManager;
        private readonly VisualizerEngine _visualizer;

        private bool _isUserSeeking;
        private bool _isShuffle;
        private int _repeatMode; // 0 = off, 1 = all, 2 = one

        public Form1()
        {
            InitializeComponent();

            _audioPlayer = new AudioPlayer();
            _playlistManager = new PlaylistManager();
            _visualizer = new VisualizerEngine(32);

            InitializeEvents();
            LoadSettings();
        }

        #region Initialization

        private void InitializeEvents()
        {
            // Audio Player Events
            _audioPlayer.PlaybackStopped += AudioPlayer_PlaybackStopped!;
            _audioPlayer.PlaybackStateChanged += AudioPlayer_StateChanged!;

            // Playlist Manager Events
            _playlistManager.PlaylistChanged += PlaylistManager_PlaylistChanged!;
            _playlistManager.CurrentIndexChanged += PlaylistManager_CurrentIndexChanged!;
        }

        #endregion

        #region File Management

        private void LoadCurrentFile()
        {
            try
            {
                string? currentFile = _playlistManager.CurrentFile;
                if (string.IsNullOrEmpty(currentFile))
                    return;

                _audioPlayer.LoadFile(currentFile);

                var metadata = MetadataReader.ReadMetadata(currentFile);
                UpdateMetadataDisplay(metadata);

                trackProgress.Maximum = (int)_audioPlayer.TotalTime.TotalSeconds;
                HighlightCurrentSong();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar arquivo: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateMetadataDisplay(AudioMetadata metadata)
        {
            lblTitle.Text = metadata.Title;
            lblArtist.Text = metadata.Artist;
            lblAlbum.Text = metadata.Album;
            lblBitrate.Text = metadata.BitrateText;
            picCover.Image = metadata.AlbumCover;
        }

        private void ClearMetadataDisplay()
        {
            lblTitle.Text = "Nenhuma música";
            lblArtist.Text = "Artista desconhecido";
            lblAlbum.Text = "Álbum desconhecido";
            lblBitrate.Text = "";
            picCover.Image = null;
        }

        #endregion

        #region Playback Controls

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_playlistManager.HasSongs)
                {
                    BtnAddFiles_Click(sender, e);
                    if (!_playlistManager.HasSongs) return;
                }

                if (!_audioPlayer.HasAudio && _playlistManager.CurrentIndex == -1)
                {
                    _playlistManager.SetCurrentIndex(0);
                    LoadCurrentFile();
                }

                if (_audioPlayer.IsPlaying)
                {
                    _audioPlayer.Pause();
                    visualizerTimer.Stop();
                    btnPlay.Text = "▶";
                    return;
                }

                _audioPlayer.Play();
                StartTimers();
                btnPlay.Text = "⏸";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reproduzir: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _audioPlayer.Stop();
            StopTimers();
            trackProgress.Value = 0;
            lblDuration.Text = "00:00 / 00:00";
            btnPlay.Text = "▶";
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (!_playlistManager.HasSongs) return;

            _playlistManager.MoveToNext(_isShuffle);
            LoadCurrentFile();
            _audioPlayer.Play();
            StartTimers();
            btnPlay.Text = "⏸";
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (!_playlistManager.HasSongs) return;

            // Se já passou 3 segundos, reinicia a música atual
            if (_audioPlayer.CurrentTime.TotalSeconds > 3)
            {
                _audioPlayer.CurrentTime = TimeSpan.Zero;
                return;
            }

            _playlistManager.MoveToPrevious(_isShuffle);
            LoadCurrentFile();
            _audioPlayer.Play();
            StartTimers();
            btnPlay.Text = "⏸";
        }

        private void BtnShuffle_Click(object sender, EventArgs e)
        {
            _isShuffle = !_isShuffle;
            btnShuffle.FillColor = _isShuffle
                ? Color.Purple
                : Color.FromArgb(45, 45, 45);
        }

        private void BtnRepeat_Click(object sender, EventArgs e)
        {
            _repeatMode = (_repeatMode + 1) % 3;

            switch (_repeatMode)
            {
                case 0: // Off
                    btnRepeat.Text = "🔁";
                    btnRepeat.FillColor = Color.FromArgb(45, 45, 45);
                    break;
                case 1: // Repeat All
                    btnRepeat.Text = "🔁";
                    btnRepeat.FillColor = Color.Purple;
                    break;
                case 2: // Repeat One
                    btnRepeat.Text = "🔂";
                    btnRepeat.FillColor = Color.Purple;
                    break;
            }
        }

        #endregion

        #region Audio Player Events

        private void AudioPlayer_PlaybackStopped(object sender, EventArgs e)
        {
            if (_repeatMode == 2) // Repeat One
            {
                this.Invoke(() =>
                {
                    _audioPlayer.CurrentTime = TimeSpan.Zero;
                    _audioPlayer.Play();
                });
            }
            else if (_repeatMode == 1 || _playlistManager.CurrentIndex < _playlistManager.Count - 1)
            {
                this.Invoke(() => BtnNext_Click(sender, EventArgs.Empty));
            }
            else
            {
                this.Invoke(() =>
                {
                    StopTimers();
                    btnPlay.Text = "▶";
                });
            }
        }

        private void AudioPlayer_StateChanged(object sender, EventArgs e)
        {
            // Atualiza UI quando o estado muda
        }

        #endregion

        #region Playlist Management

        private void BtnAddFiles_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Arquivos de áudio|*.mp3;*.wav;*.flac;*.ogg;*.aac;*.m4a;*.wma",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _playlistManager.AddRange(ofd.FileNames);
            }
        }

        private void BtnAddFolder_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _playlistManager.AddFolder(fbd.SelectedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao adicionar pasta: {ex.Message}",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClearPlaylist_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja limpar toda a playlist?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BtnStop_Click(sender, e);
                _playlistManager.Clear();
                listPlaylist.Items.Clear();
                ((IDisposable)_audioPlayer).Dispose();
                ClearMetadataDisplay();
            }
        }

        private void ListPlaylist_DoubleClick(object sender, EventArgs e)
        {
            if (listPlaylist.SelectedItems.Count == 0)
                return;

            string? selectedFile = listPlaylist.SelectedItems[0].Tag?.ToString();
            if (string.IsNullOrEmpty(selectedFile))
                return;

            _playlistManager.SetCurrentFile(selectedFile);
            LoadCurrentFile();
            _audioPlayer.Play();
            StartTimers();
            btnPlay.Text = "⏸";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            var filteredFiles = _playlistManager.Search(txtSearch.Text);
            RefreshListView(filteredFiles);
            HighlightCurrentSong();
        }

        #endregion

        #region Playlist Manager Events

        private void PlaylistManager_PlaylistChanged(object sender, EventArgs e)
        {
            RefreshListView([.. _playlistManager.Playlist]);
        }

        private void PlaylistManager_CurrentIndexChanged(object sender, EventArgs e)
        {
            HighlightCurrentSong();
        }

        private void RefreshListView(List<string> files)
        {
            listPlaylist.Items.Clear();

            foreach (var file in files)
            {
                AddToListView(file);
            }
        }

        private void AddToListView(string filePath)
        {
            string title = MetadataReader.GetTitle(filePath);
            string duration = MetadataReader.GetDuration(filePath);

            var item = new ListViewItem(title);
            item.SubItems.Add(duration);
            item.Tag = filePath;
            listPlaylist.Items.Add(item);
        }

        private void HighlightCurrentSong()
        {
            string? currentFile = _playlistManager.CurrentFile;

            foreach (ListViewItem item in listPlaylist.Items)
            {
                if (item.Tag?.ToString() == currentFile)
                {
                    item.BackColor = Color.Purple;
                    item.ForeColor = Color.White;
                    item.EnsureVisible();
                }
                else
                {
                    item.BackColor = Color.FromArgb(25, 25, 25);
                    item.ForeColor = Color.White;
                }
            }
        }

        #endregion

        #region Drag and Drop

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data!.GetDataPresent(DataFormats.FileDrop)!)
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data!.GetData(DataFormats.FileDrop)!;

            foreach (var item in items)
            {
                if (Directory.Exists(item))
                {
                    try
                    {
                        _playlistManager.AddFolder(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao adicionar pasta: {ex.Message}");
                    }
                }
                else if (File.Exists(item) && PlaylistManager.IsAudioFile(item))
                {
                    _playlistManager.Add(item);
                }
            }
        }

        private void ListPlaylist_DragEnter(object sender, DragEventArgs e)
        {
            Form1_DragEnter(sender, e);
        }

        private void ListPlaylist_DragDrop(object sender, DragEventArgs e)
        {
            Form1_DragDrop(sender, e);
        }

        #endregion

        #region Progress and Volume

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if (!_audioPlayer.HasAudio || _isUserSeeking)
                return;

            int value = (int)_audioPlayer.CurrentTime.TotalSeconds;
            if (value <= trackProgress.Maximum)
                trackProgress.Value = value;

            lblDuration.Text = $"{_audioPlayer.CurrentTime:mm\\:ss} / {_audioPlayer.TotalTime:mm\\:ss}";
        }

        private void TrackProgress_MouseDown(object sender, MouseEventArgs e)
        {
            _isUserSeeking = true;
        }

        private void TrackProgress_MouseUp(object sender, MouseEventArgs e)
        {
            _isUserSeeking = false;
            if (_audioPlayer.HasAudio)
            {
                _audioPlayer.CurrentTime = TimeSpan.FromSeconds(trackProgress.Value);
            }
        }

        private void TrackProgress_ValueChanged(object sender, EventArgs e)
        {
            if (_audioPlayer.HasAudio && _isUserSeeking)
            {
                lblDuration.Text = $"{TimeSpan.FromSeconds(trackProgress.Value):mm\\:ss} / {_audioPlayer.TotalTime:mm\\:ss}";
            }
        }

        private void TrackVolume_ValueChanged(object sender, EventArgs e)
        {
            _audioPlayer.Volume = trackVolume.Value / 100f;
            lblVolumeText.Text = $"🔊 {trackVolume.Value}%";
        }

        #endregion

        #region Visualizer

        private void VisualizerTimer_Tick(object sender, EventArgs e)
        {
            if (_audioPlayer.IsPlaying)
            {
                _visualizer.Update();
                panelVisualizer.Invalidate();
            }
        }

        private void PanelVisualizer_Paint(object sender, PaintEventArgs e)
        {
            if (!_audioPlayer.IsPlaying)
                return;

            _visualizer.Draw(e.Graphics, panelVisualizer.Width, panelVisualizer.Height);
        }

        #endregion

        #region Keyboard Shortcuts

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    BtnPlay_Click(sender, e);
                    e.Handled = true;
                    break;

                case Keys.Right:
                    if (e.Control)
                        BtnNext_Click(sender, e);
                    else
                        _audioPlayer.SeekRelative(TimeSpan.FromSeconds(5));
                    e.Handled = true;
                    break;

                case Keys.Left:
                    if (e.Control)
                        BtnPrevious_Click(sender, e);
                    else
                        _audioPlayer.SeekRelative(TimeSpan.FromSeconds(-5));
                    e.Handled = true;
                    break;

                case Keys.Up:
                    trackVolume.Value = Math.Min(100, trackVolume.Value + 5);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    trackVolume.Value = Math.Max(0, trackVolume.Value - 5);
                    e.Handled = true;
                    break;

                case Keys.O:
                    if (e.Control)
                        BtnAddFiles_Click(sender, e);
                    e.Handled = true;
                    break;

                case Keys.S:
                    if (e.Control)
                        BtnShuffle_Click(sender, e);
                    e.Handled = true;
                    break;

                case Keys.R:
                    if (e.Control)
                        BtnRepeat_Click(sender, e);
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    BtnStop_Click(sender, e);
                    e.Handled = true;
                    break;
            }
        }

        #endregion

        #region Settings

        private void LoadSettings()
        {
            try
            {
                var settings = SettingsManager.LoadSettings();

                trackVolume.Value = settings.Volume;
                _isShuffle = settings.Shuffle;
                _repeatMode = settings.RepeatMode;

                // Restore UI states
                btnShuffle.FillColor = _isShuffle
                    ? Color.Purple
                    : Color.FromArgb(45, 45, 45);

                UpdateRepeatButtonUI();

                // Load playlist
                if (settings.Playlist.Length > 0)
                {
                    _playlistManager.LoadFromArray(settings.Playlist);

                    if (settings.LastIndex >= 0 && settings.LastIndex < _playlistManager.Count)
                    {
                        _playlistManager.SetCurrentIndex(settings.LastIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar configurações: {ex.Message}");
            }
        }

        private void SaveSettings()
        {
            try
            {
                var settings = new AppSettings
                {
                    Volume = trackVolume.Value,
                    Playlist = _playlistManager.ToArray(),
                    LastIndex = _playlistManager.CurrentIndex,
                    Shuffle = _isShuffle,
                    RepeatMode = _repeatMode
                };

                SettingsManager.SaveSettings(settings,
                SettingsManager.GetOptions());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar configurações: {ex.Message}");
            }
        }

        private void UpdateRepeatButtonUI()
        {
            switch (_repeatMode)
            {
                case 0:
                    btnRepeat.Text = "🔁";
                    btnRepeat.FillColor = Color.FromArgb(45, 45, 45);
                    break;
                case 1:
                    btnRepeat.Text = "🔁";
                    btnRepeat.FillColor = Color.Purple;
                    break;
                case 2:
                    btnRepeat.Text = "🔂";
                    btnRepeat.FillColor = Color.Purple;
                    break;
            }
        }

        #endregion

        #region Timer Management

        private void StartTimers()
        {
            progressTimer.Start();
            visualizerTimer.Start();
        }

        private void StopTimers()
        {
            progressTimer.Stop();
            visualizerTimer.Stop();
        }

        #endregion

        #region Window Controls

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            StopTimers();
            ((IDisposable)_audioPlayer).Dispose();
        }

        #endregion
    }
}