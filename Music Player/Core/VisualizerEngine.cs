using System.Drawing.Drawing2D;

namespace Music_Player.Core
{
    /// <summary>
    /// Classe responsável por renderizar o visualizador de áudio
    /// </summary>
    /// <remarks>
    /// Construtor do visualizador
    /// </remarks>
    /// <param name="barCount">Número de barras do visualizador (padrão: 32)</param>
    public class VisualizerEngine(int barCount = 32)
    {
        private readonly float[] _visualizerData = new float[barCount];
        private readonly Random _random = new();
        private readonly int _barCount = barCount;

        /// <summary>
        /// Atualiza os dados do visualizador com novos valores
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < _visualizerData.Length; i++)
            {
                // Simula dados de áudio com variação aleatória
                // Valores entre 0.3 e 1.0 para aparência mais dinâmica
                _visualizerData[i] = (float)(_random.NextDouble() * 0.7 + 0.3);
            }
        }

        /// <summary>
        /// Desenha o visualizador na superfície gráfica
        /// </summary>
        /// <param name="g">Objeto Graphics para desenhar</param>
        /// <param name="width">Largura da área de desenho</param>
        /// <param name="height">Altura da área de desenho</param>
        public void Draw(Graphics g, int width, int height)
        {
            if (g == null || width <= 0 || height <= 0)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            int barWidth = width / _barCount;
            int spacing = 2;

            for (int i = 0; i < _visualizerData.Length; i++)
            {
                // Calcula a altura da barra baseada nos dados
                int barHeight = (int)(_visualizerData[i] * height * 0.8);
                int x = i * barWidth;
                int y = height - barHeight;

                // Cor gradiente baseada na intensidade
                int colorIntensity = 128 + (int)(127 * _visualizerData[i]);
                Color barColor = Color.FromArgb(colorIntensity, 0, colorIntensity);

                using SolidBrush brush = new(barColor);
                g.FillRectangle(brush, x + spacing, y, barWidth - spacing * 2, barHeight);
            }
        }

        /// <summary>
        /// Desenha o visualizador com cores personalizadas
        /// </summary>
        /// <param name="g">Objeto Graphics para desenhar</param>
        /// <param name="width">Largura da área de desenho</param>
        /// <param name="height">Altura da área de desenho</param>
        /// <param name="baseColor">Cor base das barras</param>
        public void Draw(Graphics g, int width, int height, Color baseColor)
        {
            if (g == null || width <= 0 || height <= 0)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            int barWidth = width / _barCount;
            int spacing = 2;

            for (int i = 0; i < _visualizerData.Length; i++)
            {
                int barHeight = (int)(_visualizerData[i] * height * 0.8);
                int x = i * barWidth;
                int y = height - barHeight;

                // Aplica intensidade à cor base
                int r = (int)(baseColor.R * _visualizerData[i]);
                int g_color = (int)(baseColor.G * _visualizerData[i]);
                int b = (int)(baseColor.B * _visualizerData[i]);
                Color barColor = Color.FromArgb(r, g_color, b);

                using SolidBrush brush = new(barColor);
                g.FillRectangle(brush, x + spacing, y, barWidth - spacing * 2, barHeight);
            }
        }

        /// <summary>
        /// Desenha o visualizador com gradiente de cores
        /// </summary>
        /// <param name="g">Objeto Graphics para desenhar</param>
        /// <param name="width">Largura da área de desenho</param>
        /// <param name="height">Altura da área de desenho</param>
        /// <param name="color1">Primeira cor do gradiente</param>
        /// <param name="color2">Segunda cor do gradiente</param>
        public void DrawWithGradient(Graphics g, int width, int height, Color color1, Color color2)
        {
            if (g == null || width <= 0 || height <= 0)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            int barWidth = width / _barCount;
            int spacing = 2;

            for (int i = 0; i < _visualizerData.Length; i++)
            {
                int barHeight = (int)(_visualizerData[i] * height * 0.8);
                int x = i * barWidth;
                int y = height - barHeight;

                // Interpola entre as duas cores baseado na posição
                float ratio = (float)i / _barCount;
                int r = (int)(color1.R * (1 - ratio) + color2.R * ratio);
                int g_color = (int)(color1.G * (1 - ratio) + color2.G * ratio);
                int b = (int)(color1.B * (1 - ratio) + color2.B * ratio);

                Color barColor = Color.FromArgb(r, g_color, b);

                using SolidBrush brush = new(barColor);
                g.FillRectangle(brush, x + spacing, y, barWidth - spacing * 2, barHeight);
            }
        }

        /// <summary>
        /// Reseta todos os dados do visualizador para zero
        /// </summary>
        public void Reset()
        {
            Array.Clear(_visualizerData, 0, _visualizerData.Length);
        }

        /// <summary>
        /// Define manualmente os dados do visualizador
        /// </summary>
        /// <param name="data">Array de dados (valores entre 0.0 e 1.0)</param>
        public void SetData(float[] data)
        {
            if (data == null || data.Length != _barCount)
                return;

            for (int i = 0; i < _barCount; i++)
            {
                _visualizerData[i] = Math.Clamp(data[i], 0f, 1f);
            }
        }

        /// <summary>
        /// Obtém o número de barras do visualizador
        /// </summary>
        public int BarCount => _barCount;

        /// <summary>
        /// Obtém os dados atuais do visualizador
        /// </summary>
        public float[] GetData()
        {
            return (float[])[.. _visualizerData];
        }
    }
}