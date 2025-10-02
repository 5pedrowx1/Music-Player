using System.Drawing;

namespace Music_Player.Core
{
    /// <summary>
    /// Classe que armazena os metadados de um arquivo de áudio
    /// </summary>
    public class AudioMetadata
    {
        public required string Title { get; set; }
        public required string Artist { get; set; }
        public required string Album { get; set; }
        public int Bitrate { get; set; }
        public int SampleRate { get; set; }
        public TimeSpan Duration { get; set; }
        public required Image AlbumCover { get; set; }

        public string BitrateText => Bitrate > 0
            ? $"{Bitrate} kbps • {SampleRate / 1000} kHz"
            : string.Empty;

        public string DurationText => Duration.ToString(@"mm\:ss");
    }

    /// <summary>
    /// Classe estática para leitura de metadados (ID3 tags) de arquivos de áudio
    /// </summary>
    public static class MetadataReader
    {
        /// <summary>
        /// Lê todos os metadados de um arquivo de áudio
        /// </summary>
        /// <param name="filePath">Caminho completo do arquivo</param>
        /// <returns>Objeto AudioMetadata com todas as informações</returns>
        public static AudioMetadata ReadMetadata(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Arquivo não encontrado", filePath);

            var metadata = new AudioMetadata();

            try
            {
                using var tagFile = TagLib.File.Create(filePath);

                // Informações da tag ID3
                metadata.Title = tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath);
                metadata.Artist = tagFile.Tag.FirstPerformer ?? "Artista desconhecido";
                metadata.Album = tagFile.Tag.Album ?? "Álbum desconhecido";

                // Propriedades técnicas do áudio
                metadata.Bitrate = tagFile.Properties.AudioBitrate;
                metadata.SampleRate = tagFile.Properties.AudioSampleRate;
                metadata.Duration = tagFile.Properties.Duration;

                // Capa do álbum (artwork)
                if (tagFile.Tag.Pictures.Length > 0)
                {
                    var pictureData = tagFile.Tag.Pictures[0].Data.Data;
                    using var ms = new MemoryStream(pictureData);
                    metadata.AlbumCover = Image.FromStream(ms);
                }
            }
            catch
            {
                // Se falhar ao ler metadata, usa valores padrão
                metadata.Title = Path.GetFileNameWithoutExtension(filePath);
                metadata.Artist = "Artista desconhecido";
                metadata.Album = "Álbum desconhecido";
            }

            return metadata;
        }

        /// <summary>
        /// Obtém apenas o título da música
        /// </summary>
        public static string GetTitle(string filePath)
        {
            try
            {
                using var tagFile = TagLib.File.Create(filePath);
                return tagFile.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath);
            }
            catch
            {
                return Path.GetFileNameWithoutExtension(filePath);
            }
        }

        /// <summary>
        /// Obtém apenas a duração formatada da música
        /// </summary>
        public static string GetDuration(string filePath)
        {
            try
            {
                using var tagFile = TagLib.File.Create(filePath);
                return tagFile.Properties.Duration.ToString(@"mm\:ss");
            }
            catch
            {
                return "--:--";
            }
        }

        /// <summary>
        /// Obtém apenas o artista da música
        /// </summary>
        public static string GetArtist(string filePath)
        {
            try
            {
                using var tagFile = TagLib.File.Create(filePath);
                return tagFile.Tag.FirstPerformer ?? "Artista desconhecido";
            }
            catch
            {
                return "Artista desconhecido";
            }
        }

        /// <summary>
        /// Obtém apenas o álbum da música
        /// </summary>
        public static string GetAlbum(string filePath)
        {
            try
            {
                using var tagFile = TagLib.File.Create(filePath);
                return tagFile.Tag.Album ?? "Álbum desconhecido";
            }
            catch
            {
                return "Álbum desconhecido";
            }
        }

        /// <summary>
        /// Obtém apenas a capa do álbum
        /// </summary>
        public static Image GetAlbumCover(string filePath)
        {
            try
            {
                using var tagFile = TagLib.File.Create(filePath);

                if (tagFile.Tag.Pictures.Length > 0)
                {
                    var pictureData = tagFile.Tag.Pictures[0].Data.Data;
                    using var ms = new MemoryStream(pictureData);
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                // Retorna null se não conseguir obter a capa
            }

            return null!;
        }
    }
}