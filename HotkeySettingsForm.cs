namespace AutoClicker
{
    /// <summary>
    /// Kullanıcının "Konum Al", "Başlat" ve "Durdur" işlemleri için
    /// kendi global kısayol tuşlarını atayabildiği pencere.
    /// </summary>
    public class HotkeySettingsForm : Form
    {
        public Keys CaptureKey { get; private set; }
        public Keys StartKey { get; private set; }
        public Keys StopKey { get; private set; }

        private readonly TextBox _txtCapture;
        private readonly TextBox _txtStart;
        private readonly TextBox _txtStop;

        private TextBox? _listeningBox;

        private static readonly Color ColorBg = Color.FromArgb(244, 246, 249);
        private static readonly Color ColorBorder = Color.FromArgb(223, 227, 233);
        private static readonly Color ColorHeader = Color.FromArgb(51, 65, 85);
        private static readonly Color ColorMuted = Color.FromArgb(108, 117, 125);
        private static readonly Color ColorPrimary = Color.FromArgb(59, 130, 246);
        private static readonly Color ColorPrimaryDark = Color.FromArgb(37, 99, 235);
        private static readonly Color ColorNeutral = Color.FromArgb(241, 245, 249);
        private static readonly Font FontBase = new Font("Segoe UI", 9.5f);
        private static readonly Font FontLabel = new Font("Segoe UI", 9.5f);
        private static readonly Font FontBold = new Font("Segoe UI Semibold", 9.5f, FontStyle.Bold);

        public HotkeySettingsForm(Keys currentCapture, Keys currentStart, Keys currentStop)
        {
            CaptureKey = currentCapture;
            StartKey = currentStart;
            StopKey = currentStop;

            Text = "Kısayol Ayarları";
            Font = FontBase;
            BackColor = ColorBg;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(400, 250);
            KeyPreview = true;

            var lblInfo = new Label
            {
                Text = "Atamak istediğiniz işlemin yanındaki \"Ata\" düğmesine basın,\nardından istediğiniz tuşa basın.",
                Font = FontLabel,
                ForeColor = ColorMuted,
                Location = new Point(20, 16),
                AutoSize = true
            };

            // Konum Al
            var lbl1 = new Label { Text = "Mouse Konumu Al", Font = FontBold, ForeColor = ColorHeader, Location = new Point(20, 72), AutoSize = true };
            _txtCapture = MakeReadonlyBox(currentCapture.ToString(), new Point(200, 68));
            var btn1 = MakeAssignButton(new Point(310, 66));
            btn1.Click += (s, e) => BeginListening(_txtCapture);

            // Başlat
            var lbl2 = new Label { Text = "Başlat", Font = FontBold, ForeColor = ColorHeader, Location = new Point(20, 112), AutoSize = true };
            _txtStart = MakeReadonlyBox(currentStart.ToString(), new Point(200, 108));
            var btn2 = MakeAssignButton(new Point(310, 106));
            btn2.Click += (s, e) => BeginListening(_txtStart);

            // Durdur
            var lbl3 = new Label { Text = "Durdur", Font = FontBold, ForeColor = ColorHeader, Location = new Point(20, 152), AutoSize = true };
            _txtStop = MakeReadonlyBox(currentStop.ToString(), new Point(200, 148));
            var btn3 = MakeAssignButton(new Point(310, 146));
            btn3.Click += (s, e) => BeginListening(_txtStop);

            var btnOk = new Button
            {
                Text = "Kaydet",
                Font = FontBold,
                ForeColor = Color.White,
                BackColor = ColorPrimary,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(200, 200),
                Size = new Size(90, 34),
                DialogResult = DialogResult.OK
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.FlatAppearance.MouseOverBackColor = ColorPrimaryDark;

            var btnCancel = new Button
            {
                Text = "İptal",
                Font = FontBold,
                ForeColor = ColorHeader,
                BackColor = ColorNeutral,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(300, 200),
                Size = new Size(80, 34),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderColor = ColorBorder;

            btnOk.Click += (s, e) =>
            {
                if (!TryParse(_txtCapture.Text, out var cap) ||
                    !TryParse(_txtStart.Text, out var start) ||
                    !TryParse(_txtStop.Text, out var stop))
                {
                    MessageBox.Show("Geçersiz tuş ataması.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }

                if (cap == start || cap == stop || start == stop)
                {
                    MessageBox.Show("Her işlem için farklı bir tuş seçmelisiniz.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }

                CaptureKey = cap;
                StartKey = start;
                StopKey = stop;
            };

            var separator = new Panel
            {
                BackColor = ColorBorder,
                Location = new Point(20, 190),
                Size = new Size(360, 1)
            };

            Controls.AddRange(new Control[]
            {
                lblInfo,
                lbl1, _txtCapture, btn1,
                lbl2, _txtStart, btn2,
                lbl3, _txtStop, btn3,
                separator, btnOk, btnCancel
            });

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private TextBox MakeReadonlyBox(string text, Point location)
        {
            return new TextBox
            {
                Text = text,
                Font = new Font("Consolas", 9.5f),
                TextAlign = HorizontalAlignment.Center,
                ReadOnly = true,
                BackColor = ColorNeutral,
                Location = location,
                Size = new Size(90, 27)
            };
        }

        private Button MakeAssignButton(Point location)
        {
            var btn = new Button
            {
                Text = "Ata",
                Font = FontBold,
                ForeColor = ColorHeader,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = location,
                Size = new Size(65, 30)
            };
            btn.FlatAppearance.BorderColor = ColorBorder;
            btn.FlatAppearance.MouseOverBackColor = ColorNeutral;
            return btn;
        }

        private void BeginListening(TextBox box)
        {
            _listeningBox = box;
            box.BackColor = Color.FromArgb(219, 234, 254);
            box.Text = "Tuşa basın...";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_listeningBox != null)
            {
                // Basitlik için sadece tek tuş kabul ediliyor (Alt/Ctrl/Shift kombinasyonu yok)
                _listeningBox.Text = keyData.ToString();
                _listeningBox.BackColor = Color.FromArgb(241, 245, 249);
                _listeningBox = null;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private static bool TryParse(string text, out Keys key)
        {
            return Enum.TryParse(text, out key) && key != Keys.None;
        }
    }
}
