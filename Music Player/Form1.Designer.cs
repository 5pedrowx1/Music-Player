using Guna.UI2.WinForms;

namespace Music_Player
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Guna2Button btnPlay;
        private Guna2Button btnStop;
        private Guna2Button btnPrevious;
        private Guna2Button btnNext;
        private Guna2Button btnShuffle;
        private Guna2Button btnRepeat;
        private Guna2Button btnAddFiles;
        private Guna2Button btnAddFolder;
        private Guna2Button btnClearPlaylist;
        private Guna2TrackBar trackProgress;
        private Guna2TrackBar trackVolume;
        private Guna2PictureBox picCover;
        private Label lblTitle;
        private Label lblArtist;
        private Label lblAlbum;
        private Label lblDuration;
        private Label lblVolumeText;
        private Label lblBitrate;
        private ListView listPlaylist;
        private Guna2TextBox txtSearch;
        private Guna2Panel panelVisualizer;

        private Guna2DragControl FormDrag;
        private Guna2ShadowForm ShadowForm;
        private Guna2Elipse ElipseForm;
        private Guna2Button btnMinimize;
        private Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            FormDrag = new Guna2DragControl(components);
            ShadowForm = new Guna2ShadowForm(components);
            ElipseForm = new Guna2Elipse(components);
            btnPlay = new Guna2Button();
            btnStop = new Guna2Button();
            btnPrevious = new Guna2Button();
            btnNext = new Guna2Button();
            btnShuffle = new Guna2Button();
            btnRepeat = new Guna2Button();
            btnAddFiles = new Guna2Button();
            btnAddFolder = new Guna2Button();
            btnClearPlaylist = new Guna2Button();
            btnMinimize = new Guna2Button();
            btnClose = new Guna2Button();
            trackProgress = new Guna2TrackBar();
            trackVolume = new Guna2TrackBar();
            picCover = new Guna2PictureBox();
            lblTitle = new Label();
            lblArtist = new Label();
            lblAlbum = new Label();
            lblDuration = new Label();
            lblVolumeText = new Label();
            lblBitrate = new Label();
            listPlaylist = new ListView();
            txtSearch = new Guna2TextBox();
            panelVisualizer = new Guna2Panel();
            progressTimer = new System.Windows.Forms.Timer(components);
            visualizerTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
            SuspendLayout();
            // 
            // FormDrag
            // 
            FormDrag.DockIndicatorTransparencyValue = 0.6D;
            FormDrag.TargetControl = this;
            FormDrag.UseTransparentDrag = true;
            // 
            // ShadowForm
            // 
            ShadowForm.BorderRadius = 18;
            ShadowForm.ShadowColor = Color.Purple;
            ShadowForm.TargetForm = this;
            // 
            // ElipseForm
            // 
            ElipseForm.BorderRadius = 18;
            ElipseForm.TargetControl = this;
            // 
            // btnPlay
            // 
            btnPlay.BorderRadius = 8;
            btnPlay.CustomizableEdges = customizableEdges13;
            btnPlay.FillColor = Color.Purple;
            btnPlay.Font = new Font("Segoe UI", 14F);
            btnPlay.ForeColor = Color.White;
            btnPlay.Location = new Point(440, 295);
            btnPlay.Name = "btnPlay";
            btnPlay.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnPlay.Size = new Size(60, 60);
            btnPlay.TabIndex = 7;
            btnPlay.Text = "▶";
            btnPlay.Click += BtnPlay_Click;
            // 
            // btnStop
            // 
            btnStop.BorderColor = Color.Purple;
            btnStop.BorderRadius = 8;
            btnStop.BorderThickness = 1;
            btnStop.CustomizableEdges = customizableEdges19;
            btnStop.FillColor = Color.FromArgb(45, 45, 45);
            btnStop.Font = new Font("Segoe UI", 10F);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(658, 300);
            btnStop.Name = "btnStop";
            btnStop.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnStop.Size = new Size(50, 50);
            btnStop.TabIndex = 11;
            btnStop.Text = "⏹";
            btnStop.Click += BtnStop_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.BorderColor = Color.Purple;
            btnPrevious.BorderRadius = 8;
            btnPrevious.BorderThickness = 1;
            btnPrevious.CustomizableEdges = customizableEdges11;
            btnPrevious.FillColor = Color.FromArgb(60, 60, 60);
            btnPrevious.Font = new Font("Segoe UI", 12F);
            btnPrevious.ForeColor = Color.White;
            btnPrevious.Location = new Point(370, 300);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnPrevious.Size = new Size(50, 50);
            btnPrevious.TabIndex = 6;
            btnPrevious.Text = "⏮";
            btnPrevious.Click += BtnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.BorderColor = Color.Purple;
            btnNext.BorderRadius = 8;
            btnNext.BorderThickness = 1;
            btnNext.CustomizableEdges = customizableEdges15;
            btnNext.FillColor = Color.FromArgb(60, 60, 60);
            btnNext.Font = new Font("Segoe UI", 12F);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(518, 300);
            btnNext.Name = "btnNext";
            btnNext.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnNext.Size = new Size(50, 50);
            btnNext.TabIndex = 9;
            btnNext.Text = "⏭";
            btnNext.Click += BtnNext_Click;
            // 
            // btnShuffle
            // 
            btnShuffle.BorderColor = Color.Purple;
            btnShuffle.BorderRadius = 8;
            btnShuffle.BorderThickness = 1;
            btnShuffle.CustomizableEdges = customizableEdges9;
            btnShuffle.FillColor = Color.FromArgb(45, 45, 45);
            btnShuffle.Font = new Font("Segoe UI", 10F);
            btnShuffle.ForeColor = Color.White;
            btnShuffle.Location = new Point(300, 300);
            btnShuffle.Name = "btnShuffle";
            btnShuffle.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnShuffle.Size = new Size(50, 50);
            btnShuffle.TabIndex = 5;
            btnShuffle.Text = "🔀";
            btnShuffle.Click += BtnShuffle_Click;
            // 
            // btnRepeat
            // 
            btnRepeat.BorderColor = Color.Purple;
            btnRepeat.BorderRadius = 8;
            btnRepeat.BorderThickness = 1;
            btnRepeat.CustomizableEdges = customizableEdges17;
            btnRepeat.FillColor = Color.FromArgb(45, 45, 45);
            btnRepeat.Font = new Font("Segoe UI", 10F);
            btnRepeat.ForeColor = Color.White;
            btnRepeat.Location = new Point(588, 300);
            btnRepeat.Name = "btnRepeat";
            btnRepeat.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnRepeat.Size = new Size(50, 50);
            btnRepeat.TabIndex = 10;
            btnRepeat.Text = "🔁";
            btnRepeat.Click += BtnRepeat_Click;
            // 
            // btnAddFiles
            // 
            btnAddFiles.BorderRadius = 8;
            btnAddFiles.CustomizableEdges = customizableEdges21;
            btnAddFiles.FillColor = Color.Purple;
            btnAddFiles.Font = new Font("Segoe UI", 10F);
            btnAddFiles.ForeColor = Color.White;
            btnAddFiles.Location = new Point(820, 70);
            btnAddFiles.Name = "btnAddFiles";
            btnAddFiles.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnAddFiles.Size = new Size(120, 40);
            btnAddFiles.TabIndex = 16;
            btnAddFiles.Text = "➕ Adicionar";
            btnAddFiles.Click += BtnAddFiles_Click;
            // 
            // btnAddFolder
            // 
            btnAddFolder.BorderColor = Color.Purple;
            btnAddFolder.BorderRadius = 8;
            btnAddFolder.BorderThickness = 1;
            btnAddFolder.CustomizableEdges = customizableEdges23;
            btnAddFolder.FillColor = Color.FromArgb(60, 60, 60);
            btnAddFolder.Font = new Font("Segoe UI", 10F);
            btnAddFolder.ForeColor = Color.White;
            btnAddFolder.Location = new Point(1080, 70);
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnAddFolder.Size = new Size(120, 40);
            btnAddFolder.TabIndex = 17;
            btnAddFolder.Text = "📁 Pasta";
            btnAddFolder.Click += BtnAddFolder_Click;
            // 
            // btnClearPlaylist
            // 
            btnClearPlaylist.BorderColor = Color.Purple;
            btnClearPlaylist.BorderRadius = 8;
            btnClearPlaylist.BorderThickness = 1;
            btnClearPlaylist.CustomizableEdges = customizableEdges25;
            btnClearPlaylist.FillColor = Color.FromArgb(60, 60, 60);
            btnClearPlaylist.Font = new Font("Segoe UI", 10F);
            btnClearPlaylist.ForeColor = Color.White;
            btnClearPlaylist.Location = new Point(955, 70);
            btnClearPlaylist.Name = "btnClearPlaylist";
            btnClearPlaylist.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnClearPlaylist.Size = new Size(110, 40);
            btnClearPlaylist.TabIndex = 18;
            btnClearPlaylist.Text = "🗑️ Limpar";
            btnClearPlaylist.Click += BtnClearPlaylist_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.BorderColor = Color.Purple;
            btnMinimize.BorderRadius = 8;
            btnMinimize.BorderThickness = 1;
            btnMinimize.CustomizableEdges = customizableEdges3;
            btnMinimize.FillColor = Color.Transparent;
            btnMinimize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Location = new Point(1100, 10);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.PressedColor = Color.DarkMagenta;
            btnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMinimize.Size = new Size(40, 40);
            btnMinimize.TabIndex = 21;
            btnMinimize.Text = "—";
            btnMinimize.Click += BtnMinimize_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderColor = Color.Purple;
            btnClose.BorderRadius = 8;
            btnClose.BorderThickness = 1;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1150, 10);
            btnClose.Name = "btnClose";
            btnClose.PressedColor = Color.DarkMagenta;
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(40, 40);
            btnClose.TabIndex = 20;
            btnClose.Text = "✕";
            btnClose.Click += BtnClose_Click;
            // 
            // trackProgress
            // 
            trackProgress.FillColor = Color.Gray;
            trackProgress.Location = new Point(300, 375);
            trackProgress.Name = "trackProgress";
            trackProgress.Size = new Size(480, 23);
            trackProgress.TabIndex = 12;
            trackProgress.ThumbColor = Color.Purple;
            trackProgress.ValueChanged += TrackProgress_ValueChanged;
            trackProgress.MouseDown += TrackProgress_MouseDown;
            trackProgress.MouseUp += TrackProgress_MouseUp;
            // 
            // trackVolume
            // 
            trackVolume.FillColor = Color.Gray;
            trackVolume.Location = new Point(380, 435);
            trackVolume.Name = "trackVolume";
            trackVolume.Size = new Size(200, 23);
            trackVolume.TabIndex = 15;
            trackVolume.ThumbColor = Color.Purple;
            trackVolume.ValueChanged += TrackVolume_ValueChanged;
            // 
            // picCover
            // 
            picCover.BackColor = Color.FromArgb(30, 30, 30);
            picCover.BorderRadius = 10;
            picCover.CustomizableEdges = customizableEdges5;
            picCover.FillColor = Color.MediumVioletRed;
            picCover.ImageRotate = 0F;
            picCover.Location = new Point(30, 70);
            picCover.Name = "picCover";
            picCover.ShadowDecoration.CustomizableEdges = customizableEdges6;
            picCover.Size = new Size(250, 250);
            picCover.SizeMode = PictureBoxSizeMode.Zoom;
            picCover.TabIndex = 0;
            picCover.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Purple;
            lblTitle.Location = new Point(300, 70);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Nenhuma música";
            // 
            // lblArtist
            // 
            lblArtist.Font = new Font("Segoe UI", 12F);
            lblArtist.ForeColor = Color.Gray;
            lblArtist.Location = new Point(300, 110);
            lblArtist.Name = "lblArtist";
            lblArtist.Size = new Size(400, 25);
            lblArtist.TabIndex = 2;
            lblArtist.Text = "Artista desconhecido";
            // 
            // lblAlbum
            // 
            lblAlbum.Font = new Font("Segoe UI", 11F);
            lblAlbum.ForeColor = Color.Gray;
            lblAlbum.Location = new Point(300, 140);
            lblAlbum.Name = "lblAlbum";
            lblAlbum.Size = new Size(400, 25);
            lblAlbum.TabIndex = 3;
            lblAlbum.Text = "Álbum desconhecido";
            // 
            // lblDuration
            // 
            lblDuration.Font = new Font("Segoe UI", 10F);
            lblDuration.ForeColor = Color.Purple;
            lblDuration.Location = new Point(300, 405);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(150, 20);
            lblDuration.TabIndex = 13;
            lblDuration.Text = "00:00 / 00:00";
            // 
            // lblVolumeText
            // 
            lblVolumeText.Font = new Font("Segoe UI", 9F);
            lblVolumeText.ForeColor = Color.Gray;
            lblVolumeText.Location = new Point(300, 435);
            lblVolumeText.Name = "lblVolumeText";
            lblVolumeText.Size = new Size(80, 20);
            lblVolumeText.TabIndex = 14;
            lblVolumeText.Text = "🔊 50%";
            // 
            // lblBitrate
            // 
            lblBitrate.Font = new Font("Segoe UI", 9F);
            lblBitrate.ForeColor = Color.DarkGray;
            lblBitrate.Location = new Point(300, 170);
            lblBitrate.Name = "lblBitrate";
            lblBitrate.Size = new Size(200, 20);
            lblBitrate.TabIndex = 4;
            // 
            // listPlaylist
            // 
            listPlaylist.AllowDrop = true;
            listPlaylist.BackColor = Color.FromArgb(25, 25, 25);
            listPlaylist.BorderStyle = BorderStyle.None;
            listPlaylist.ForeColor = Color.White;
            listPlaylist.FullRowSelect = true;
            listPlaylist.Location = new Point(820, 170);
            listPlaylist.Name = "listPlaylist";
            listPlaylist.Size = new Size(370, 400);
            listPlaylist.TabIndex = 20;
            listPlaylist.UseCompatibleStateImageBehavior = false;
            listPlaylist.View = View.Tile;
            listPlaylist.DragDrop += ListPlaylist_DragDrop;
            listPlaylist.DragEnter += ListPlaylist_DragEnter;
            listPlaylist.DoubleClick += ListPlaylist_DoubleClick;
            // 
            // txtSearch
            // 
            txtSearch.BorderColor = Color.Purple;
            txtSearch.BorderRadius = 10;
            txtSearch.CustomizableEdges = customizableEdges27;
            txtSearch.DefaultText = "";
            txtSearch.FillColor = Color.FromArgb(18, 18, 18);
            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(820, 120);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍 Pesquisar...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges28;
            txtSearch.Size = new Size(370, 36);
            txtSearch.TabIndex = 19;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // panelVisualizer
            // 
            panelVisualizer.BackColor = Color.FromArgb(25, 25, 25);
            panelVisualizer.BorderColor = Color.Purple;
            panelVisualizer.BorderRadius = 8;
            panelVisualizer.BorderThickness = 2;
            panelVisualizer.CustomizableEdges = customizableEdges7;
            panelVisualizer.Location = new Point(300, 200);
            panelVisualizer.Name = "panelVisualizer";
            panelVisualizer.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelVisualizer.Size = new Size(400, 80);
            panelVisualizer.TabIndex = 22;
            panelVisualizer.Paint += PanelVisualizer_Paint;
            // 
            // progressTimer
            // 
            progressTimer.Interval = 500;
            progressTimer.Tick += ProgressTimer_Tick;
            // 
            // visualizerTimer
            // 
            visualizerTimer.Interval = 50;
            visualizerTimer.Tick += VisualizerTimer_Tick;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(1200, 600);
            Controls.Add(btnClose);
            Controls.Add(btnMinimize);
            Controls.Add(picCover);
            Controls.Add(lblTitle);
            Controls.Add(lblArtist);
            Controls.Add(lblAlbum);
            Controls.Add(lblBitrate);
            Controls.Add(panelVisualizer);
            Controls.Add(btnShuffle);
            Controls.Add(btnPrevious);
            Controls.Add(btnPlay);
            Controls.Add(btnNext);
            Controls.Add(btnRepeat);
            Controls.Add(btnStop);
            Controls.Add(trackProgress);
            Controls.Add(lblDuration);
            Controls.Add(lblVolumeText);
            Controls.Add(trackVolume);
            Controls.Add(btnAddFiles);
            Controls.Add(btnAddFolder);
            Controls.Add(btnClearPlaylist);
            Controls.Add(txtSearch);
            Controls.Add(listPlaylist);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Music Player";
            FormClosing += Form1_FormClosing;
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)picCover).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.Timer visualizerTimer;
    }
}