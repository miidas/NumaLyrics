using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using static NumaLyrics.Utils.Native.GDI32;
using static NumaLyrics.Utils.Native.USER32;

namespace NumaLyrics.Lyrics
{
    class LayeredLyricsWindow : Form
    {
        private static float gDpiY = -1;

        public string LyricsText;

        private Bitmap bitmap;
        //private float gDpiY;
        private byte alpha = 255;

        // Lyrics style
        private string FontFamily;
        private float FontSize;
        private int FontStyle;
        private string FontColor;
        private string FontOutlineColor;
        private float FontOutlineWidth;

        // Display position
        private int DisplayIndex;
        private float DisplayPositionX;
        private float DisplayPositionY;

        private bool PreConfigured;

        public LayeredLyricsWindow(float posX, float posY, float fontSize)
        {
            this.PreConfigured = true;
            Initialize(null, posX, posY, fontSize);
        }

        public LayeredLyricsWindow(string str)
        {
            Initialize(str, null, null, null);
        }

        private void Initialize(string str, float? posX, float? posY, float? fontSize)
        {
            this.LyricsText = str;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;

            // Get DpiY (4~8ms)
            if (gDpiY == -1)
            {
                using (Graphics g = this.CreateGraphics())
                {
                    gDpiY = g.DpiY;
                }
            }

            if (posX.HasValue && posY.HasValue)
            {
                this.DisplayPositionX = posX.Value;
                this.DisplayPositionY = posY.Value;
            }

            if (fontSize.HasValue)
            {
                this.FontSize = fontSize.Value;
            }

            this.LoadConfig(); // 0ms
            if (str != null) this.PerformDraw(); // 6~10ms
        }

        public void Redraw()
        {
            this.LoadConfig();
            this.PerformDraw();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle |
                    WS_EX_LAYERED |
                    WS_EX_NOACTIVATE |
                    WS_EX_TOOLWINDOW |
                    WS_EX_TOPMOST |
                    WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private void PerformDraw()
        {
            // Draw lyrics
            using (GraphicsPath gp = new GraphicsPath())
            {
                StringFormat sf = new StringFormat();

                Pen drawPen = new Pen(ColorTranslator.FromHtml(FontOutlineColor), FontOutlineWidth);
                drawPen.LineJoin = LineJoin.Bevel;
                Brush fillBrush = new SolidBrush(ColorTranslator.FromHtml(FontColor));
                FontFamily fontFamily = new FontFamily(FontFamily);

                gp.AddString("|", fontFamily, FontStyle, gDpiY * FontSize / 72f, new Point(0, 0), sf);
                RectangleF spaceBound = gp.GetBounds();
                gp.Reset();

                gp.AddString(this.LyricsText, fontFamily, FontStyle, gDpiY * FontSize / 72f, new Point(0, 0), sf);

                RectangleF rect = gp.GetBounds();

                this.Width = (int)Math.Round(rect.Width + spaceBound.Width * 15);
                this.Height = (int)Math.Round(spaceBound.Height * 1.5);

                Rectangle bounds;

                if (Screen.AllScreens.Length <= this.DisplayIndex)
                {
                    // Fall back to the primary screen due to a temporary reason
                    bounds = Screen.PrimaryScreen.Bounds;
                }
                else
                {
                    bounds = Screen.AllScreens[this.DisplayIndex].Bounds;
                }

                var lp = new Point(
                    (int)(bounds.Left + (Screen.GetBounds(this).Width * this.DisplayPositionX) - (this.Width / 2)),
                    (int)(bounds.Top + Screen.GetBounds(this).Height * this.DisplayPositionY)
                );

                this.Location = lp;

                Matrix matrix = new Matrix();
                matrix.Translate(-rect.X, -rect.Y);
                matrix.Translate((this.Width - rect.Width) / 2, (this.Height - rect.Height) / 2);
                gp.Transform(matrix);
                matrix.Dispose();

                bitmap = new Bitmap(this.Width, this.Height);
                Graphics g = Graphics.FromImage(bitmap);

                Color color = Color.FromArgb((int)(255 * 0.7), 0, 0, 0); // Opacity = 0.7
                g.Clear(color);

                // TODO: check these options
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                g.DrawPath(drawPen, gp);
                g.FillPath(fillBrush, gp);

                g.Dispose();
                sf.Dispose();
                drawPen.Dispose();
                fillBrush.Dispose();
                fontFamily.Dispose();
            }

            UpdateWindow();
        }

        private void UpdateWindow()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr gHdc = g.GetHdc();

            Graphics gBitmap = Graphics.FromImage(bitmap);
            IntPtr bitmapHdc = gBitmap.GetHdc();

            IntPtr oldhbmp = SelectObject(bitmapHdc, bitmap.GetHbitmap(Color.FromArgb(0)));

            BLENDFUNCTION blend = new BLENDFUNCTION();
            blend.BlendOp = 0;
            blend.BlendFlags = 0;
            blend.SourceConstantAlpha = alpha;
            blend.AlphaFormat = 1;

            Point pptSrc = new Point(0, 0);
            Point pptDst = new Point(this.Left, this.Top);
            Size pSize = new Size(this.Width, this.Height);

            // dwFlags
            // ULW_ALPHA = 0x00000002
            // Use pblend as the blend function. If the display mode is 256 colors or less, the effect of this value is the same as the effect of ULW_OPAQUE.
            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-updatelayeredwindow#parameters
            UpdateLayeredWindow(this.Handle, gHdc, ref pptDst, ref pSize, bitmapHdc, ref pptSrc, 0, ref blend, 2);

            DeleteObject(SelectObject(bitmapHdc, oldhbmp));

            g.ReleaseHdc(gHdc);
            g.Dispose();
            gBitmap.ReleaseHdc(bitmapHdc);
            gBitmap.Dispose();
            bitmap.Dispose();
        }

        public void LoadConfig()
        {
            if (!PreConfigured) this.FontSize = AppConfig.FontSize;
            this.FontFamily = AppConfig.FontFamily;
            this.FontStyle = AppConfig.FontStyle;
            this.FontColor = AppConfig.FontColor;
            this.FontOutlineColor = AppConfig.FontOutlineColor;
            this.FontOutlineWidth = AppConfig.FontOutlineWidth;
            this.DisplayIndex = AppConfig.DisplayIndex;

            if (!PreConfigured)
            {
                this.DisplayPositionX = AppConfig.DisplayPositionX;
                this.DisplayPositionY = AppConfig.DisplayPositionY;
            }
        }
    }
}
