using System.Runtime.InteropServices;

namespace AutoClicker
{
    public partial class Form1 : Form
    {
        #region WinAPI

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        // Global hotkey sabitleri
        private const int WM_HOTKEY = 0x0312;
        private const uint MOD_NONE = 0x0000;

        private const int HOTKEY_ID_CAPTURE = 9001;
        private const int HOTKEY_ID_START = 9002;
        private const int HOTKEY_ID_STOP = 9003;

        // Kullanıcının atayabildiği tuşlar (varsayılan: F2 / F5 / F6)
        private Keys _keyCapture = Keys.F2;
        private Keys _keyStart = Keys.F5;
        private Keys _keyStop = Keys.F6;

        private bool _hotkeysRegistered;

        #endregion

        #region Model

        private class ClickPoint
        {
            public int X;
            public int Y;
            public int DelayMs;
            public string ClickType = "Sol Tık"; // "Sol Tık" | "Sağ Tık" | "Çift Tık"
        }

        #endregion

        private readonly List<ClickPoint> _points = new();
        private CancellationTokenSource? _cts;
        private bool _isRunning;

        private Button btnHotkeySettings = null!;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += (s, e) => StopRun();

            AddHotkeySettingsButton();
            UpdateHotkeyLabels();
        }

        /// <summary>
        /// Designer'a dokunmadan, "Kısayol Ayarla" butonunu üst gruba ekler.
        /// </summary>
        private void AddHotkeySettingsButton()
        {
            btnHotkeySettings = new Button
            {
                Text = "⚙ Kısayol Ayarla",
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                BackColor = Color.FromArgb(241, 245, 249),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(610, 22),
                Size = new Size(145, 30)
            };
            btnHotkeySettings.FlatAppearance.BorderColor = Color.FromArgb(223, 227, 233);
            btnHotkeySettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            btnHotkeySettings.Click += (s, e) => OpenHotkeySettings();
            grpRun.Controls.Add(btnHotkeySettings);
        }

        private void UpdateHotkeyLabels()
        {
            btnGetMousePos.Text = $"🎯 Mouse Konumu Al ({_keyCapture})";
            btnStart.Text = $"▶ BAŞLAT ({_keyStart})";
            btnStop.Text = $"⏹ DURDUR ({_keyStop})";
        }

        private void OpenHotkeySettings()
        {
            using var dlg = new HotkeySettingsForm(_keyCapture, _keyStart, _keyStop);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            // Eski kayıtları kaldır
            if (_hotkeysRegistered)
            {
                UnregisterHotKey(this.Handle, HOTKEY_ID_CAPTURE);
                UnregisterHotKey(this.Handle, HOTKEY_ID_START);
                UnregisterHotKey(this.Handle, HOTKEY_ID_STOP);
            }

            _keyCapture = dlg.CaptureKey;
            _keyStart = dlg.StartKey;
            _keyStop = dlg.StopKey;

            RegisterAllHotkeys();
            UpdateHotkeyLabels();
        }

        #region Global Hotkeys (kullanıcı tarafından atanabilir)

        private void RegisterAllHotkeys()
        {
            bool okCapture = RegisterHotKey(this.Handle, HOTKEY_ID_CAPTURE, MOD_NONE, (uint)_keyCapture);
            bool okStart = RegisterHotKey(this.Handle, HOTKEY_ID_START, MOD_NONE, (uint)_keyStart);
            bool okStop = RegisterHotKey(this.Handle, HOTKEY_ID_STOP, MOD_NONE, (uint)_keyStop);

            _hotkeysRegistered = true;

            if (!okCapture || !okStart || !okStop)
            {
                MessageBox.Show(
                    "Bazı global kısayollar kaydedilemedi.\n" +
                    "Başka bir uygulama bu tuşları zaten kullanıyor olabilir.\n" +
                    "\"⚙ Kısayol Ayarla\" ile farklı tuşlar seçebilirsiniz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RegisterAllHotkeys();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_hotkeysRegistered)
            {
                UnregisterHotKey(this.Handle, HOTKEY_ID_CAPTURE);
                UnregisterHotKey(this.Handle, HOTKEY_ID_START);
                UnregisterHotKey(this.Handle, HOTKEY_ID_STOP);
            }
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                switch (id)
                {
                    case HOTKEY_ID_CAPTURE:
                        CaptureMousePosition();
                        break;
                    case HOTKEY_ID_START:
                        if (!_isRunning) StartRun();
                        break;
                    case HOTKEY_ID_STOP:
                        if (_isRunning) StopRun();
                        break;
                }
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Koordinat Ekle paneli

        private void BtnGetMousePos_Click(object? sender, EventArgs e)
        {
            CaptureMousePosition();
        }

        private void CaptureMousePosition()
        {
            GetCursorPos(out POINT p);
            lblCurrentPos.Text = $"Mouse Konum:  X = {p.X}   Y = {p.Y}";
            nudX.Value = Math.Min(Math.Max(p.X, nudX.Minimum), nudX.Maximum);
            nudY.Value = Math.Min(Math.Max(p.Y, nudY.Minimum), nudY.Maximum);
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var point = new ClickPoint
            {
                X = (int)nudX.Value,
                Y = (int)nudY.Value,
                DelayMs = (int)nudDelay.Value,
                ClickType = cmbClickType.SelectedItem?.ToString() ?? "Sol Tık"
            };

            _points.Add(point);
            RefreshGrid();
        }

        #endregion

        #region Tıklama Listesi paneli

        private void RefreshGrid()
        {
            dgvCoordinates.Rows.Clear();
            for (int i = 0; i < _points.Count; i++)
            {
                var p = _points[i];
                dgvCoordinates.Rows.Add(i + 1, p.X, p.Y, p.DelayMs, p.ClickType);
            }
        }

        private int SelectedIndex()
        {
            if (dgvCoordinates.SelectedRows.Count == 0) return -1;
            return dgvCoordinates.SelectedRows[0].Index;
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            int idx = SelectedIndex();
            if (idx < 0 || idx >= _points.Count) return;

            _points.RemoveAt(idx);
            RefreshGrid();
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            if (_points.Count == 0) return;

            var result = MessageBox.Show("Tüm liste silinsin mi?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _points.Clear();
                RefreshGrid();
            }
        }

        private void MoveItem(int direction)
        {
            int idx = SelectedIndex();
            if (idx < 0) return;

            int newIdx = idx + direction;
            if (newIdx < 0 || newIdx >= _points.Count) return;

            (_points[idx], _points[newIdx]) = (_points[newIdx], _points[idx]);
            RefreshGrid();
            dgvCoordinates.Rows[newIdx].Selected = true;
        }

        #endregion

        #region Çalıştır paneli

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartRun();
        }

        private void BtnStop_Click(object? sender, EventArgs e)
        {
            StopRun();
        }

        private void StartRun()
        {
            if (_isRunning) return;

            if (_points.Count == 0)
            {
                MessageBox.Show("Listede tıklama noktası yok. Önce koordinat ekleyin.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isRunning = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "▶ Çalışıyor";
            lblStatus.ForeColor = Color.FromArgb(22, 163, 74);

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            bool infinite = chkLoop.Checked;
            int repeatCount = (int)nudRepeat.Value;

            _ = RunLoopAsync(infinite, repeatCount, token);
        }

        private void StopRun()
        {
            if (!_isRunning) return;

            _cts?.Cancel();
            _isRunning = false;

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "⏸ Durduruldu";
            lblStatus.ForeColor = Color.FromArgb(108, 117, 125);
        }

        private async Task RunLoopAsync(bool infinite, int repeatCount, CancellationToken token)
        {
            try
            {
                int iterations = 0;
                while (!token.IsCancellationRequested && (infinite || iterations < repeatCount))
                {
                    foreach (var point in _points)
                    {
                        if (token.IsCancellationRequested) break;

                        SetCursorPos(point.X, point.Y);
                        PerformClick(point.ClickType);

                        try
                        {
                            await Task.Delay(point.DelayMs, token);
                        }
                        catch (TaskCanceledException)
                        {
                            break;
                        }
                    }

                    iterations++;
                }
            }
            finally
            {
                // UI thread'e geri dön
                if (!this.IsDisposed)
                {
                    this.BeginInvoke(() =>
                    {
                        _isRunning = false;
                        btnStart.Enabled = true;
                        btnStop.Enabled = false;
                        lblStatus.Text = "✅ Tamamlandı";
                        lblStatus.ForeColor = Color.FromArgb(108, 117, 125);
                    });
                }
            }
        }

        private void PerformClick(string clickType)
        {
            switch (clickType)
            {
                case "Sol Tık":
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case "Sağ Tık":
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case "Çift Tık":
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
            }
        }

        #endregion
    }
}
