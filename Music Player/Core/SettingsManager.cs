using System.Text.Json;

namespace Music_Player.Core
{
    /// <summary>
    /// Classe que representa as configurações do aplicativo
    /// </summary>
    public class AppSettings
    {
        public int Volume { get; set; } = 50;
        public string[] Playlist { get; set; } = [];
        public int LastIndex { get; set; } = -1;
        public bool Shuffle { get; set; } = false;
        public int RepeatMode { get; set; } = 0;
    }

    /// <summary>
    /// Classe estática para gerenciar a persistência de configurações
    /// </summary>
    public static class SettingsManager
    {
        private const string SettingsFile = "settings.json";

        /// <summary>
        /// Carrega as configurações do arquivo JSON
        /// </summary>
        /// <returns>Objeto AppSettings com as configurações salvas ou padrões</returns>
        public static AppSettings LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsFile))
                    return new AppSettings();

                string json = File.ReadAllText(SettingsFile);

                if (string.IsNullOrWhiteSpace(json))
                    return new AppSettings();

                var settings = JsonSerializer.Deserialize<AppSettings>(json);

                return settings ?? new AppSettings();
            }
            catch (Exception ex)
            {
                // Log do erro (opcional)
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar configurações: {ex.Message}");
                return new AppSettings();
            }
        }

        public static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Salva as configurações em arquivo JSON
        /// </summary>
        /// <param name="settings">Objeto AppSettings para salvar</param>
        public static void SaveSettings(AppSettings settings, JsonSerializerOptions options)
        {
            ArgumentNullException.ThrowIfNull(settings);

            try
            {
                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(SettingsFile, json);
            }
            catch (Exception ex)
            {
                // Log do erro mas não lança exceção
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar configurações: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica se o arquivo de configurações existe
        /// </summary>
        public static bool SettingsFileExists()
        {
            return File.Exists(SettingsFile);
        }

        /// <summary>
        /// Deleta o arquivo de configurações
        /// </summary>
        public static void DeleteSettings()
        {
            try
            {
                if (File.Exists(SettingsFile))
                    File.Delete(SettingsFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao deletar configurações: {ex.Message}");
            }
        }

        /// <summary>
        /// Reseta as configurações para os valores padrão
        /// </summary>
        public static AppSettings ResetToDefaults()
        {
            var defaultSettings = new AppSettings();
            SaveSettings(defaultSettings, GetOptions());
            return defaultSettings;
        }

        /// <summary>
        /// Valida se as configurações estão corretas
        /// </summary>
        public static AppSettings ValidateSettings(AppSettings settings)
        {
            if (settings == null)
                return new AppSettings();

            // Valida volume (0-100)
            if (settings.Volume < 0 || settings.Volume > 100)
                settings.Volume = 50;

            // Valida repeat mode (0-2)
            if (settings.RepeatMode < 0 || settings.RepeatMode > 2)
                settings.RepeatMode = 0;

            // Valida playlist (remove arquivos que não existem mais)
            if (settings.Playlist != null && settings.Playlist.Length > 0)
            {
                settings.Playlist = [.. settings.Playlist.Where(File.Exists)];
            }
            else
            {
                settings.Playlist = [];
            }

            // Valida last index
            if (settings.LastIndex >= settings.Playlist.Length)
                settings.LastIndex = -1;

            return settings;
        }
    }
}