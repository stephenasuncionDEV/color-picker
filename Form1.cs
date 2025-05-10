using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace color_picker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        const int VK_MBUTTON = 0x04;
        const int VK_ESC = 0x1B;
        const int VK_BACKTICK = 0xC0;

        public Form1()
        {
            InitializeComponent();
        }

        void TogglePicking()
        {
            if (worker.Enabled)
            {
                worker.Stop();
                statusLabel.Text = "Idle";
                pickBtn.Text = "Pick Color";
                toggleMenuItem.Text = "Pick Color";
                return;
            }

            worker.Start();
            statusLabel.Text = "Running (press ` or scroll wheel to pick)";
            pickBtn.Text = "Stop Picking";
            toggleMenuItem.Text = "Stop Picking";
        }

        private void pickBtn_Click(object sender, EventArgs e)
        {
            TogglePicking();
        }

        private void worker_Tick(object sender, EventArgs e)
        {
            if (GetCursorPos(out Point cursorPosition))
            {
                int zoomAreaSize = 15;
                int zoomScale = 10;
                int captureSize = zoomAreaSize;

                Rectangle captureRect = new Rectangle(
                    cursorPosition.X - captureSize / 2,
                    cursorPosition.Y - captureSize / 2,
                    captureSize,
                    captureSize
                );

                using (Bitmap bmp = new Bitmap(captureSize, captureSize))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size);
                    }

                    Color pixelColor = bmp.GetPixel(captureSize / 2, captureSize / 2);

                    hexTxt.Text = $"#{pixelColor.R:X2}{pixelColor.G:X2}{pixelColor.B:X2}";
                    rgbTxt.Text = $"RGB({pixelColor.R}, {pixelColor.G}, {pixelColor.B})";

                    Bitmap zoomedBmp = new Bitmap(captureSize * zoomScale, captureSize * zoomScale);
                    using (Graphics gZoom = Graphics.FromImage(zoomedBmp))
                    {
                        gZoom.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        gZoom.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                        gZoom.DrawImage(bmp, new Rectangle(0, 0, zoomedBmp.Width, zoomedBmp.Height));
                    }

                    colorDisplay.Image = zoomedBmp;
                    colorDisplay.Invalidate();

                    if ((GetAsyncKeyState(VK_ESC) & 0x8000) != 0)
                    {
                        TogglePicking();
                    }

                    if ((GetAsyncKeyState(VK_MBUTTON) & 0x8000) != 0 || (GetAsyncKeyState(VK_BACKTICK) & 0x8000) != 0)
                    {
                        var lastItem = savedList.Items.Count > 0 ? savedList.Items[savedList.Items.Count - 1] : null;
                        if (lastItem != null && lastItem.ToString() == hexTxt.Text)
                        {
                            return;
                        }

                        savedList.Items.Add(hexTxt.Text);

                        if (cancelOnPick.Checked)
                        {
                            TogglePicking();
                        }

                        Clipboard.SetText(hexTxt.Text);

                        if (showOnPickMenuItem.Checked && FormWindowState.Minimized == this.WindowState)
                        {
                            this.Show();
                            this.WindowState = FormWindowState.Normal;
                        }

                        if (notifyOnPickMenuItem.Checked)
                        {
                            notifyIcon1.BalloonTipTitle = "Color Picked";
                            notifyIcon1.BalloonTipText = $"Picked color: {hexTxt.Text}";
                            notifyIcon1.ShowBalloonTip(300);
                        }

                        if (savedList.Items.Count > 0)
                        {
                            savedList.TopIndex = savedList.Items.Count - 1;
                            savedList.SelectedIndex = savedList.Items.Count - 1;
                        }
                    }
                }
            }
        }

        private void hexTxt_DoubleClick(object sender, EventArgs e)
        {
            hexTxt.SelectAll();
            hexTxt.Copy();
        }

        private void rgbTxt_DoubleClick(object sender, EventArgs e)
        {
            rgbTxt.SelectAll();
            rgbTxt.Copy();
        }

        private void colorDisplay_Paint(object sender, PaintEventArgs e)
        {
            var pb = sender as PictureBox;
            if (pb?.Image == null) return;

            int zoomedSize = pb.Image.Width; // e.g., 100 (if 10x10 at 10x zoom)
            int pixels = pb.Image.Width / 10; // number of pixels across (adjust as needed)
            int scale = zoomedSize / pixels; // how big each pixel appears

            using (Pen gridPen = new Pen(Color.Black, 1))
            using (Pen centerPen = new Pen(Color.Red, 2))
            {
                gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                for (int i = 0; i <= pixels; i++)
                {
                    int pos = i * scale;
                    e.Graphics.DrawLine(gridPen, pos, 0, pos, zoomedSize);
                    e.Graphics.DrawLine(gridPen, 0, pos, zoomedSize, pos);
                }

                int centerPixel = pixels / 2;
                Rectangle centerRect = new Rectangle(centerPixel * scale, centerPixel * scale, scale, scale);
                e.Graphics.DrawRectangle(centerPen, centerRect);
            }
        }

        private void savedList_DoubleClick(object sender, EventArgs e)
        {
            if (savedList.SelectedItem != null)
            {
                string selectedColor = savedList.SelectedItem.ToString();
                Clipboard.SetText(selectedColor);
                MessageBox.Show($"Copied {selectedColor} to clipboard", "Color Picker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.BalloonTipTitle = "Color Picker is still running";
                notifyIcon1.BalloonTipText = "Click to re-open the app";
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                ToolStripItem item = e.ClickedItem;
                if (item.Name == "openMenuItem")
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }
                else if (item.Name == "toggleMenuItem")
                {
                    TogglePicking();
                }
                else if (item.Name == "showOnPickMenuItem")
                {
                    showOnPickMenuItem.Checked = !showOnPickMenuItem.Checked;
                }
                else if (item.Name == "notifyOnPickMenuItem")
                {
                    notifyOnPickMenuItem.Checked = !notifyOnPickMenuItem.Checked;
                }
                else if (item.Name == "closeMenuItem")
                {
                    this.Close();
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            savedList.Items.Clear();
        }
    }
}
