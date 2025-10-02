namespace Music_Player.Core
{
    /// <summary>
    /// Classe responsável por gerenciar a playlist de músicas
    /// </summary>
    public class PlaylistManager
    {
        private readonly List<string> _playlist = [];
        private int _currentIndex = -1;
        private readonly Random _random = new();

        // Eventos
        public event EventHandler? PlaylistChanged;
        public event EventHandler? CurrentIndexChanged;

        // Propriedades públicas
        public IReadOnlyList<string> Playlist => _playlist.AsReadOnly();
        public int CurrentIndex => _currentIndex;
        public int Count => _playlist.Count;
        public bool HasSongs => _playlist.Count > 0;
        public string? CurrentFile => _currentIndex >= 0 && _currentIndex < _playlist.Count
            ? _playlist[_currentIndex]
            : null;

        // Extensões de áudio suportadas
        private static readonly string[] AudioExtensions =
        [
            ".mp3", ".wav", ".flac", ".ogg", ".aac", ".m4a", ".wma"
        ];

        /// <summary>
        /// Adiciona um arquivo à playlist
        /// </summary>
        public void Add(string filePath)
        {
            if (File.Exists(filePath) && IsAudioFile(filePath) && !_playlist.Contains(filePath))
            {
                _playlist.Add(filePath);
                PlaylistChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adiciona múltiplos arquivos à playlist
        /// </summary>
        public void AddRange(IEnumerable<string> files)
        {
            bool added = false;
            foreach (var file in files)
            {
                if (File.Exists(file) && IsAudioFile(file) && !_playlist.Contains(file))
                {
                    _playlist.Add(file);
                    added = true;
                }
            }
            if (added)
                PlaylistChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Adiciona todos os arquivos de áudio de uma pasta (recursivamente)
        /// </summary>
        public void AddFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Pasta não encontrada: {folderPath}");

            var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Where(IsAudioFile);

            AddRange(files);
        }

        /// <summary>
        /// Remove um arquivo da playlist
        /// </summary>
        public void Remove(string filePath)
        {
            int index = _playlist.IndexOf(filePath);
            if (index >= 0)
            {
                _playlist.RemoveAt(index);

                // Ajusta o índice atual se necessário
                if (_currentIndex == index)
                    _currentIndex = -1;
                else if (_currentIndex > index)
                    _currentIndex--;

                PlaylistChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Remove um arquivo por índice
        /// </summary>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _playlist.Count)
            {
                _playlist.RemoveAt(index);

                // Ajusta o índice atual se necessário
                if (_currentIndex == index)
                    _currentIndex = -1;
                else if (_currentIndex > index)
                    _currentIndex--;

                PlaylistChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Limpa toda a playlist
        /// </summary>
        public void Clear()
        {
            _playlist.Clear();
            _currentIndex = -1;
            PlaylistChanged?.Invoke(this, EventArgs.Empty);
            CurrentIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Define o índice atual da playlist
        /// </summary>
        public void SetCurrentIndex(int index)
        {
            if (index >= -1 && index < _playlist.Count)
            {
                _currentIndex = index;
                CurrentIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Move para a próxima música
        /// </summary>
        /// <param name="shuffle">Se true, escolhe uma música aleatória</param>
        public bool MoveToNext(bool shuffle = false)
        {
            if (_playlist.Count == 0)
                return false;

            if (shuffle)
            {
                _currentIndex = _random.Next(_playlist.Count);
            }
            else
            {
                _currentIndex = (_currentIndex + 1) % _playlist.Count;
            }

            CurrentIndexChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Move para a música anterior
        /// </summary>
        /// <param name="shuffle">Se true, escolhe uma música aleatória</param>
        public bool MoveToPrevious(bool shuffle = false)
        {
            if (_playlist.Count == 0)
                return false;

            if (shuffle)
            {
                _currentIndex = _random.Next(_playlist.Count);
            }
            else
            {
                _currentIndex--;
                if (_currentIndex < 0)
                    _currentIndex = _playlist.Count - 1;
            }

            CurrentIndexChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Define a música atual pelo caminho do arquivo
        /// </summary>
        public void SetCurrentFile(string filePath)
        {
            int index = _playlist.IndexOf(filePath);
            if (index >= 0)
            {
                _currentIndex = index;
                CurrentIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Busca músicas na playlist
        /// </summary>
        /// <param name="searchText">Texto para buscar nos nomes dos arquivos</param>
        /// <returns>Lista filtrada de arquivos</returns>
        public List<string> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return [.. _playlist];

            searchText = searchText.ToLower();
            return [.. _playlist.Where(file => Path.GetFileNameWithoutExtension(file).Contains(searchText, StringComparison.CurrentCultureIgnoreCase))];
        }

        /// <summary>
        /// Verifica se um arquivo é um arquivo de áudio suportado
        /// </summary>
        public static bool IsAudioFile(string filePath)
        {
            string? extension = Path.GetExtension(filePath)?.ToLower();
            return !string.IsNullOrEmpty(extension) && AudioExtensions.Contains(extension);
        }

        /// <summary>
        /// Converte a playlist para array
        /// </summary>
        public string[] ToArray() => [.. _playlist];

        /// <summary>
        /// Carrega uma playlist de um array de arquivos
        /// </summary>
        public void LoadFromArray(string[] files)
        {
            _playlist.Clear();
            AddRange(files.Where(File.Exists));
        }

        /// <summary>
        /// Embaralha a playlist (altera a ordem dos arquivos)
        /// </summary>
        public void Shuffle()
        {
            if (_playlist.Count <= 1)
                return;

            string? currentFile = CurrentFile;

            // Algoritmo Fisher-Yates para embaralhar
            for (int i = _playlist.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_playlist[j], _playlist[i]) = (_playlist[i], _playlist[j]);
            }

            // Atualiza o índice atual
            if (currentFile != null)
            {
                _currentIndex = _playlist.IndexOf(currentFile);
            }

            PlaylistChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Obtém o arquivo em um índice específico
        /// </summary>
        public string GetFileAt(int index)
        {
            if (index >= 0 && index < _playlist.Count)
                return _playlist[index];
            return null!;
        }
    }
}