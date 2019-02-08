using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace osuThumb
{
    public partial class MainForm : Form
    {
        private string appdataPath = "";
        private string osuFolder = "";
        private string thumbFolder = "";

        private List<object> renderObjects = new List<object>();
        private Font defaultFont = new Font("Arial", 24);

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Checks if required directories exist
            appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            osuFolder = appdataPath + @"\osu!";

            if (!Directory.Exists(osuFolder))
            {
                MessageBox.Show("ERROR: osu folder wasn't found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                thumbFolder = osuFolder + @"\Data\bt";
                if (!Directory.Exists(thumbFolder))
                {
                    MessageBox.Show("ERROR: osu's bt folder wasn't found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            //Sets font using a FontDialog
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                defaultFont = fontDialog.Font;
            }
        }

        //THUMBNAIL GENERATOR
        private void generateButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = preview.CreateGraphics())
            {
                g.Clear(Color.Black);

                //Renders each object in the list
                foreach (object renderObject in renderObjects)
                {

                    if (renderObject.GetType() == typeof(ImageObject))
                    {
                        ImageObject io = (ImageObject)renderObject;
                        //Checks for variables in path property
                        string save = io.path;
                        if (io.path == "%BG%")
                        {
                            io.path = thumbFolder + @"\" + idBox.Text + "l.jpg";
                            save = "%BG%";
                        }
                        else if (io.path == "%RANKING%")
                        {
                            io.path = "res/ranking/ranking-" + rankingBox.Text.ToLower() + ".png";
                            save = "%RANKING%";
                        }
                        else if (io.path == "%MODS%")
                        {
                            io.path = "res/mods/selection-mod-doubletime" + ".png";
                            save = "%MODS%";
                        }
                        io.LoadImage();
                        io.path = save;

                        Bitmap bitmap = ColorTint((Bitmap)io.image, io.color);

                        Rectangle rect = new Rectangle(
                            (int)(io.rect.Y * preview.Height),
                            (int)(io.rect.X * preview.Width),
                            (int)(io.rect.Width * (io.canvasSize ? preview.Width : bitmap.Width)),
                            (int)(io.rect.Height * (io.canvasSize ? preview.Width : bitmap.Height))
                        );

                        g.DrawImage(bitmap, rect);
                    }
                    else if (renderObject.GetType() == typeof(TextObject))
                    {
                        TextObject to = (TextObject)renderObject;
                        string text = to.text;
                        //Checks for variables in text property
                        if (to.text == "%ACC%")
                        {
                            float f_acc = float.Parse(accBox.Text);
                            text = f_acc.ToString("0.00") + "%";
                            text = text.Replace(',', '.');
                        }
                        else if (to.text == "%SR%")
                        {
                            text = starBox.Text + "*";
                            text = text.Replace(',', '.');
                        }

                        SolidBrush brush = new SolidBrush(to.color);
                        Font font = new Font(defaultFont.FontFamily, to.textSize == -1 ? defaultFont.Size : to.textSize);
                        Point position = new Point(
                            (int)(to.position.X * preview.Width),
                            (int)(to.position.Y * preview.Height)
                        );
                        g.DrawString(text, font, brush, position);
                    }
                    else if (renderObject.GetType() == typeof(RectangleObject))
                    {
                        RectangleObject ro = (RectangleObject)renderObject;

                        SolidBrush brush = new SolidBrush(ro.color);
                        Rectangle rect = new Rectangle(
                            (int)(ro.rect.X * preview.Width),
                            (int)(ro.rect.Y * preview.Height),
                            (int)(ro.rect.Width * preview.Width),
                            (int)(ro.rect.Height * preview.Height)
                        );
                        g.FillRectangle(brush, rect);
                    }
                }

            }
        }

        private Bitmap ColorTint (Bitmap src, Color tint)
        {
            BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[data.Stride * data.Height];
            Marshal.Copy(data.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            float r, g, b;
            for (int i = 0; (i + 4) < pixelBuffer.Length; i += 4) {
                b = pixelBuffer[i]   * ((float)tint.B / 255);
                g = pixelBuffer[i+1] * ((float)tint.G / 255);
                r = pixelBuffer[i+2] * ((float)tint.R / 255);

                if (b > 255) { b = 255; }
                if (g > 255) { g = 255; }
                if (r > 255) { r = 255; }

                pixelBuffer[i]   = (byte)b;
                pixelBuffer[i+1] = (byte)g;
                pixelBuffer[i+2] = (byte)r;
            }

            Bitmap result = new Bitmap(src.Width, src.Height);
            BitmapData resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            result.UnlockBits(resultData);

            return result;
        }

        //LAYOUT LOADER
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Layout file|*.layout";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                LoadLayout(filePath);
            }

        }

        private void LoadLayout (string filePath)
        {
            if (!File.Exists(filePath)) { return; }

            StreamReader sr = new StreamReader(filePath);

            string current = "";

            TextObject textObject = null;
            RectangleObject rectangleObject = null;
            ImageObject imageObject = null;
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line == string.Empty) { continue; }

                string noSpaces = line.Replace(" ", "");

                //Looks for object start
                if (line[0] == '{')
                {
                    string[] data = line.Split(' ');

                    if (data[1] == "text")           { current = "text";      textObject = new TextObject();           }
                    else if (data[1] == "rectangle") { current = "rectangle"; rectangleObject = new RectangleObject(); }
                    else if (data[1] == "image")     { current = "image";     imageObject = new ImageObject();         }
                }

                if (current == "text")
                {
                    //Looks for object properties
                    if (noSpaces.StartsWith("text:"))
                    {
                        string[] data = noSpaces.Split(':');
                        textObject.text = data[1];
                    }
                    else if (noSpaces.StartsWith("position:"))
                    {
                        string[] data = noSpaces.Split(':');
                        data[1] = data[1].Substring(1, data[1].Length - 2);

                        string[] split = data[1].Split(',');
                        

                        float x = float.Parse(split[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(split[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);

                        textObject.position = new PointF(x, y);
                    }
                    else if (noSpaces.StartsWith("color:"))
                    {
                        string[] data = noSpaces.Split(':');
                        data[1] = data[1].Substring(1, data[1].Length - 2);

                        string[] split = data[1].Split(',');

                        int r = int.Parse(split[0]);
                        int g = int.Parse(split[1]);
                        int b = int.Parse(split[2]);
                        int a = int.Parse(split[3]);

                        textObject.color = Color.FromArgb(a, r, g, b);
                    }
                    else if (noSpaces.StartsWith("font-size:"))
                    {
                        string[] data = noSpaces.Split(':');
                        int size = int.Parse(data[1]);

                        textObject.textSize = size;
                    }

                    //End object
                    if (noSpaces[0] == '}')
                    {
                        renderObjects.Add(textObject);
                        textObject = null;
                        current = "";
                    }
                }
                else if (current == "rectangle")
                {
                    //Looks for object properties
                    if (noSpaces.StartsWith("rect:"))
                    {
                        string[] data = noSpaces.Split(':');
                        data[1] = data[1].Substring(1, data[1].Length - 2);

                        string[] split = data[1].Split(',');
                        

                        float x = float.Parse(split[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(split[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float w = float.Parse(split[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float h = float.Parse(split[3], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);

                        rectangleObject.rect = new RectangleF(x, y, w, h);
                    }
                    else if (noSpaces.StartsWith("color:"))
                    {
                        string[] data = noSpaces.Split(':');
                        data[1] = data[1].Substring(1, data[1].Length - 2);

                        string[] split = data[1].Split(',');

                        int r = int.Parse(split[0]);
                        int g = int.Parse(split[1]);
                        int b = int.Parse(split[2]);
                        int a = int.Parse(split[3]);

                        rectangleObject.color = Color.FromArgb(a, r, g, b);
                    }

                    //End object
                    if (noSpaces[0] == '}')
                    {
                        renderObjects.Add(rectangleObject);
                        rectangleObject = null;
                        current = "";
                    }
                }
                else if (current == "image")
                {
                    //Looks for object properties
                    if (noSpaces.StartsWith("path:"))
                    {
                        string[] data = noSpaces.Split(':');
                        imageObject.path = data[1];
                    }
                    else if (noSpaces.StartsWith("rect:"))
                    {
                        string[] data = noSpaces.Split(':');
                        data[1] = data[1].Substring(1, data[1].Length - 2);

                        string[] split = data[1].Split(',');

                        float x = float.Parse(split[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(split[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float w = float.Parse(split[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                        float h = float.Parse(split[3], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);

                        imageObject.rect = new RectangleF(x, y, w, h);
                    }
                    else if (noSpaces.StartsWith("color:"))
                    {
                        string[] data = noSpaces.Split(':');
                        data[1] = data[1].Substring(1, data[1].Length - 2);

                        string[] split = data[1].Split(',');

                        int r = int.Parse(split[0]);
                        int g = int.Parse(split[1]);
                        int b = int.Parse(split[2]);
                        int a = int.Parse(split[3]);

                        imageObject.color = Color.FromArgb(a, r, g, b);
                    }
                    else if (noSpaces.StartsWith("canvas-size:"))
                    {
                        string[] data = noSpaces.Split(':');
                        bool canvasSize = false;
                        if (data[1] == "true")
                        {
                            canvasSize = true;
                        }
                        imageObject.canvasSize = canvasSize;
                     }

                    //End object
                    if (noSpaces[0] == '}')
                    {
                        renderObjects.Add(imageObject);
                        imageObject = null;
                        current = "";
                    }
                }
            }

            sr.Close();
            sr.Dispose();
        }
    }
}
