namespace AutoClicker
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // ===== RENK PALETİ =====
        private static readonly Color ColorBg = Color.FromArgb(244, 246, 249);
        private static readonly Color ColorPanel = Color.White;
        private static readonly Color ColorBorder = Color.FromArgb(223, 227, 233);
        private static readonly Color ColorText = Color.FromArgb(33, 37, 41);
        private static readonly Color ColorMuted = Color.FromArgb(108, 117, 125);
        private static readonly Color ColorPrimary = Color.FromArgb(59, 130, 246);
        private static readonly Color ColorPrimaryDark = Color.FromArgb(37, 99, 235);
        private static readonly Color ColorSuccess = Color.FromArgb(34, 197, 94);
        private static readonly Color ColorSuccessDark = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorDanger = Color.FromArgb(239, 68, 68);
        private static readonly Color ColorDangerDark = Color.FromArgb(220, 38, 38);
        private static readonly Color ColorNeutral = Color.FromArgb(241, 245, 249);
        private static readonly Color ColorHeader = Color.FromArgb(51, 65, 85);

        private static readonly Font FontBase = new Font("Segoe UI", 9.5f);
        private static readonly Font FontLabel = new Font("Segoe UI", 9.5f, FontStyle.Regular);
        private static readonly Font FontGroupTitle = new Font("Segoe UI Semibold", 10f, FontStyle.Bold);
        private static readonly Font FontButton = new Font("Segoe UI Semibold", 10f, FontStyle.Bold);
        private static readonly Font FontStatus = new Font("Segoe UI Semibold", 12f, FontStyle.Bold);
        private static readonly Font FontMono = new Font("Consolas", 9.5f);

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ===== GRUP KUTULARI =====
            grpAdd = new GroupBox();
            grpList = new GroupBox();
            grpRun = new GroupBox();

            // ===== EKLE PANELİ =====
            lblX = new Label();
            lblY = new Label();
            lblDelay = new Label();
            lblClickType = new Label();
            nudX = new NumericUpDown();
            nudY = new NumericUpDown();
            nudDelay = new NumericUpDown();
            cmbClickType = new ComboBox();
            btnAdd = new Button();
            btnGetMousePos = new Button();
            lblCurrentPos = new Label();

            // ===== LİSTE PANELİ =====
            dgvCoordinates = new DataGridView();
            btnRemove = new Button();
            btnClear = new Button();
            btnMoveUp = new Button();
            btnMoveDown = new Button();

            // ===== ÇALIŞTIR PANELİ =====
            lblRepeat = new Label();
            nudRepeat = new NumericUpDown();
            chkLoop = new CheckBox();
            btnStart = new Button();
            btnStop = new Button();
            lblStatus = new Label();

            // ===== SUSPEND =====
            grpAdd.SuspendLayout();
            grpList.SuspendLayout();
            grpRun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCoordinates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRepeat).BeginInit();
            this.SuspendLayout();

            // =====================
            // grpAdd
            // =====================
            grpAdd.Text = "  Koordinat Ekle";
            grpAdd.Font = FontGroupTitle;
            grpAdd.ForeColor = ColorHeader;
            grpAdd.BackColor = ColorPanel;
            grpAdd.Location = new Point(16, 16);
            grpAdd.Size = new Size(768, 118);
            grpAdd.Padding = new Padding(12, 8, 12, 12);
            grpAdd.Controls.AddRange(new Control[]
            {
                lblX, nudX, lblY, nudY, lblDelay, nudDelay,
                lblClickType, cmbClickType, btnAdd, btnGetMousePos, lblCurrentPos
            });

            // X Label
            lblX.Text = "X";
            lblX.Font = FontLabel;
            lblX.ForeColor = ColorMuted;
            lblX.Location = new Point(14, 30);
            lblX.AutoSize = true;

            // nudX
            StyleNumeric(nudX);
            nudX.Location = new Point(14, 48);
            nudX.Size = new Size(75, 27);
            nudX.Maximum = 9999;
            nudX.Minimum = 0;
            nudX.Value = 0;

            // Y Label
            lblY.Text = "Y";
            lblY.Font = FontLabel;
            lblY.ForeColor = ColorMuted;
            lblY.Location = new Point(102, 30);
            lblY.AutoSize = true;

            // nudY
            StyleNumeric(nudY);
            nudY.Location = new Point(102, 48);
            nudY.Size = new Size(75, 27);
            nudY.Maximum = 9999;
            nudY.Minimum = 0;
            nudY.Value = 0;

            // Delay Label
            lblDelay.Text = "Gecikme (ms)";
            lblDelay.Font = FontLabel;
            lblDelay.ForeColor = ColorMuted;
            lblDelay.Location = new Point(190, 30);
            lblDelay.AutoSize = true;

            // nudDelay
            StyleNumeric(nudDelay);
            nudDelay.Location = new Point(190, 48);
            nudDelay.Size = new Size(90, 27);
            nudDelay.Maximum = 60000;
            nudDelay.Minimum = 50;
            nudDelay.Value = 500;
            nudDelay.Increment = 100;

            // ClickType Label
            lblClickType.Text = "Tıklama Tipi";
            lblClickType.Font = FontLabel;
            lblClickType.ForeColor = ColorMuted;
            lblClickType.Location = new Point(295, 30);
            lblClickType.AutoSize = true;

            // cmbClickType
            cmbClickType.Font = FontBase;
            cmbClickType.Location = new Point(295, 48);
            cmbClickType.Size = new Size(120, 27);
            cmbClickType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClickType.FlatStyle = FlatStyle.Flat;
            cmbClickType.Items.AddRange(new object[] { "Sol Tık", "Sağ Tık", "Çift Tık" });
            cmbClickType.SelectedIndex = 0;

            // btnAdd
            btnAdd.Text = "➕  Listeye Ekle";
            btnAdd.Font = FontButton;
            btnAdd.ForeColor = Color.White;
            btnAdd.BackColor = ColorPrimary;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = ColorPrimaryDark;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Location = new Point(430, 46);
            btnAdd.Size = new Size(150, 33);
            btnAdd.Click += new EventHandler(BtnAdd_Click);

            // btnGetMousePos
            btnGetMousePos.Text = "🎯  Mouse Konumu Al";
            btnGetMousePos.Font = FontButton;
            btnGetMousePos.ForeColor = ColorHeader;
            btnGetMousePos.BackColor = ColorNeutral;
            btnGetMousePos.FlatStyle = FlatStyle.Flat;
            btnGetMousePos.FlatAppearance.BorderColor = ColorBorder;
            btnGetMousePos.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            btnGetMousePos.Cursor = Cursors.Hand;
            btnGetMousePos.Location = new Point(580, 46);
            btnGetMousePos.Size = new Size(170, 33);
            btnGetMousePos.Click += new EventHandler(BtnGetMousePos_Click);

            // lblCurrentPos
            lblCurrentPos.Text = "Mouse Konum:  X = 0   Y = 0";
            lblCurrentPos.Font = FontMono;
            lblCurrentPos.ForeColor = ColorMuted;
            lblCurrentPos.Location = new Point(430, 84);
            lblCurrentPos.AutoSize = true;

            // =====================
            // grpList
            // =====================
            grpList.Text = "  Tıklama Listesi";
            grpList.Font = FontGroupTitle;
            grpList.ForeColor = ColorHeader;
            grpList.BackColor = ColorPanel;
            grpList.Location = new Point(16, 144);
            grpList.Size = new Size(768, 266);
            grpList.Padding = new Padding(12, 8, 12, 12);
            grpList.Controls.AddRange(new Control[]
            {
                dgvCoordinates, btnRemove, btnClear, btnMoveUp, btnMoveDown
            });

            // dgvCoordinates
            dgvCoordinates.Location = new Point(14, 28);
            dgvCoordinates.Size = new Size(740, 190);
            dgvCoordinates.AllowUserToAddRows = false;
            dgvCoordinates.AllowUserToResizeRows = false;
            dgvCoordinates.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCoordinates.ReadOnly = true;
            dgvCoordinates.RowHeadersVisible = false;
            dgvCoordinates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCoordinates.BackgroundColor = ColorPanel;
            dgvCoordinates.BorderStyle = BorderStyle.FixedSingle;
            dgvCoordinates.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCoordinates.GridColor = ColorBorder;
            dgvCoordinates.MultiSelect = false;
            dgvCoordinates.Font = FontBase;
            dgvCoordinates.RowTemplate.Height = 30;
            dgvCoordinates.EnableHeadersVisualStyles = false;
            dgvCoordinates.ColumnHeadersDefaultCellStyle.BackColor = ColorHeader;
            dgvCoordinates.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCoordinates.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5f, FontStyle.Bold);
            dgvCoordinates.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorHeader;
            dgvCoordinates.ColumnHeadersHeight = 34;
            dgvCoordinates.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCoordinates.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvCoordinates.DefaultCellStyle.SelectionForeColor = ColorText;
            dgvCoordinates.DefaultCellStyle.Padding = new Padding(4, 0, 0, 0);
            dgvCoordinates.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvCoordinates.Columns.Add("colNo", "#");
            dgvCoordinates.Columns.Add("colX", "X");
            dgvCoordinates.Columns.Add("colY", "Y");
            dgvCoordinates.Columns.Add("colDelay", "Gecikme (ms)");
            dgvCoordinates.Columns.Add("colType", "Tıklama Tipi");
            dgvCoordinates.Columns["colNo"].FillWeight = 30;
            dgvCoordinates.Columns["colX"].FillWeight = 80;
            dgvCoordinates.Columns["colY"].FillWeight = 80;
            dgvCoordinates.Columns["colDelay"].FillWeight = 100;
            dgvCoordinates.Columns["colType"].FillWeight = 100;

            // btnRemove
            btnRemove.Text = "🗑  Seçileni Sil";
            btnRemove.Font = FontButton;
            btnRemove.ForeColor = ColorDangerDark;
            btnRemove.BackColor = Color.FromArgb(254, 226, 226);
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatAppearance.MouseOverBackColor = Color.FromArgb(252, 165, 165);
            btnRemove.Cursor = Cursors.Hand;
            btnRemove.Location = new Point(14, 226);
            btnRemove.Size = new Size(130, 32);
            btnRemove.Click += new EventHandler(BtnRemove_Click);

            // btnClear
            btnClear.Text = "🧹  Tümünü Temizle";
            btnClear.Font = FontButton;
            btnClear.ForeColor = ColorHeader;
            btnClear.BackColor = ColorNeutral;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderColor = ColorBorder;
            btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            btnClear.Cursor = Cursors.Hand;
            btnClear.Location = new Point(154, 226);
            btnClear.Size = new Size(160, 32);
            btnClear.Click += new EventHandler(BtnClear_Click);

            // btnMoveUp
            btnMoveUp.Text = "▲";
            btnMoveUp.Font = FontButton;
            btnMoveUp.ForeColor = ColorHeader;
            btnMoveUp.BackColor = ColorNeutral;
            btnMoveUp.FlatStyle = FlatStyle.Flat;
            btnMoveUp.FlatAppearance.BorderColor = ColorBorder;
            btnMoveUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            btnMoveUp.Cursor = Cursors.Hand;
            btnMoveUp.Location = new Point(624, 226);
            btnMoveUp.Size = new Size(60, 32);
            btnMoveUp.Click += (s, e) => MoveItem(-1);

            // btnMoveDown
            btnMoveDown.Text = "▼";
            btnMoveDown.Font = FontButton;
            btnMoveDown.ForeColor = ColorHeader;
            btnMoveDown.BackColor = ColorNeutral;
            btnMoveDown.FlatStyle = FlatStyle.Flat;
            btnMoveDown.FlatAppearance.BorderColor = ColorBorder;
            btnMoveDown.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            btnMoveDown.Cursor = Cursors.Hand;
            btnMoveDown.Location = new Point(694, 226);
            btnMoveDown.Size = new Size(60, 32);
            btnMoveDown.Click += (s, e) => MoveItem(1);

            // =====================
            // grpRun
            // =====================
            grpRun.Text = "  Çalıştır";
            grpRun.Font = FontGroupTitle;
            grpRun.ForeColor = ColorHeader;
            grpRun.BackColor = ColorPanel;
            grpRun.Location = new Point(16, 420);
            grpRun.Size = new Size(768, 122);
            grpRun.Padding = new Padding(12, 8, 12, 12);
            grpRun.Controls.AddRange(new Control[]
            {
                lblRepeat, nudRepeat, chkLoop, btnStart, btnStop, lblStatus
            });

            // lblRepeat
            lblRepeat.Text = "Tekrar Sayısı";
            lblRepeat.Font = FontLabel;
            lblRepeat.ForeColor = ColorMuted;
            lblRepeat.Location = new Point(14, 30);
            lblRepeat.AutoSize = true;

            // nudRepeat
            StyleNumeric(nudRepeat);
            nudRepeat.Location = new Point(14, 48);
            nudRepeat.Size = new Size(80, 27);
            nudRepeat.Minimum = 1;
            nudRepeat.Maximum = 9999;
            nudRepeat.Value = 1;

            // chkLoop
            chkLoop.Text = "Sonsuz Döngü";
            chkLoop.Font = FontLabel;
            chkLoop.ForeColor = ColorText;
            chkLoop.Location = new Point(110, 51);
            chkLoop.AutoSize = true;
            chkLoop.CheckedChanged += (s, e) => nudRepeat.Enabled = !chkLoop.Checked;

            // btnStart
            btnStart.Text = "▶   BAŞLAT";
            btnStart.Font = FontButton;
            btnStart.ForeColor = Color.White;
            btnStart.BackColor = ColorSuccess;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatAppearance.MouseOverBackColor = ColorSuccessDark;
            btnStart.Cursor = Cursors.Hand;
            btnStart.Location = new Point(14, 84);
            btnStart.Size = new Size(220, 30);
            btnStart.Click += new EventHandler(BtnStart_Click);

            // btnStop
            btnStop.Text = "⏹   DURDUR";
            btnStop.Font = FontButton;
            btnStop.ForeColor = Color.White;
            btnStop.BackColor = ColorDanger;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatAppearance.MouseOverBackColor = ColorDangerDark;
            btnStop.Cursor = Cursors.Hand;
            btnStop.Enabled = false;
            btnStop.Location = new Point(244, 84);
            btnStop.Size = new Size(220, 30);
            btnStop.Click += new EventHandler(BtnStop_Click);

            // lblStatus
            lblStatus.Text = "⏸  Hazır";
            lblStatus.Font = FontStatus;
            lblStatus.ForeColor = ColorMuted;
            lblStatus.Location = new Point(500, 60);
            lblStatus.AutoSize = true;

            // =====================
            // FORM
            // =====================
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = FontBase;
            this.BackColor = ColorBg;
            this.ClientSize = new Size(800, 558);
            this.Text = "🖱️  Auto Clicker Pro";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;
            this.Controls.AddRange(new Control[] { grpAdd, grpList, grpRun });

            // ===== RESUME =====
            grpAdd.ResumeLayout(false);
            grpAdd.PerformLayout();
            grpList.ResumeLayout(false);
            grpRun.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCoordinates).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRepeat).EndInit();
            this.ResumeLayout(false);
        }

        private static void StyleNumeric(NumericUpDown nud)
        {
            nud.Font = FontBase;
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.TextAlign = HorizontalAlignment.Center;
        }

        #endregion

        // ===== KONTROL DEĞİŞKENLERİ =====
        private GroupBox grpAdd, grpList, grpRun;
        private Label lblX, lblY, lblDelay, lblClickType, lblCurrentPos, lblRepeat, lblStatus;
        private NumericUpDown nudX, nudY, nudDelay, nudRepeat;
        private ComboBox cmbClickType;
        private Button btnAdd, btnGetMousePos, btnRemove, btnClear, btnMoveUp, btnMoveDown, btnStart, btnStop;
        private DataGridView dgvCoordinates;
        private CheckBox chkLoop;
    }
}
