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

        public Form1()
        {
            InitializeComponent();
        }

        private void pickBtn_Click(object sender, EventArgs e)
        {
            if (worker.Enabled)
            {
                worker.Stop();
                statusLabel.Text = "Idle";
            }
            else
            {
                worker.Start();
                statusLabel.Text = "Running (press scroll wheel to pick)";
            }
        }

        private void worker_Tick(object sender, EventArgs e)
        {
            if (GetCursorPos(out Point cursorPosition))
            {
                int zoomAreaSize = 15; // 10x10 pixels around the cursor
                int zoomScale = 10;    // Scale each pixel to 10x10
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

                    // update text fields
                    hexTxt.Text = $"#{pixelColor.R:X2}{pixelColor.G:X2}{pixelColor.B:X2}";
                    rgbTxt.Text = $"RGB({pixelColor.R}, {pixelColor.G}, {pixelColor.B})";

                    // create zoomed image
                    Bitmap zoomedBmp = new Bitmap(captureSize * zoomScale, captureSize * zoomScale);
                    using (Graphics gZoom = Graphics.FromImage(zoomedBmp))
                    {
                        gZoom.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        gZoom.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                        gZoom.DrawImage(bmp, new Rectangle(0, 0, zoomedBmp.Width, zoomedBmp.Height));
                    }

                    colorDisplay.Image = zoomedBmp;
                    colorDisplay.Invalidate();

                    // detect mouse wheel click
                    if ((GetAsyncKeyState(VK_MBUTTON) & 0x8000) != 0)
                    {
                        savedList.Items.Add(hexTxt.Text);
                        if (cancelOnPick.Checked)
                        {
                            worker.Stop();
                            statusLabel.Text = "Idle";
                        }

                        Clipboard.SetText(hexTxt.Text);
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

                // draw grid
                for (int i = 0; i <= pixels; i++)
                {
                    int pos = i * scale;
                    // vertical lines
                    e.Graphics.DrawLine(gridPen, pos, 0, pos, zoomedSize);
                    // horizontal lines
                    e.Graphics.DrawLine(gridPen, 0, pos, zoomedSize, pos);
                }

                // highlight center pixel with red rectangle
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
    }
}
