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
        private Font font = new Font("Arial", 24);

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
                font = fontDialog.Font;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = preview.CreateGraphics())
            {
                //Renders each object in the list
                foreach (object renderObject in renderObjects)
                {

                    if (renderObject.GetType() == typeof(ImageObject))
                    {
                        ImageObject io = (ImageObject)renderObject;
                        //Checks for variables in path property
                        if (io.path == "%BG%")
                        {
                            io.path = thumbFolder + @"\" + idBox.Text + "l.jpg";
                        }
                        io.LoadImage();

                        Bitmap bitmap = ColorTint((Bitmap)io.image, io.color);

                        g.DrawImage(bitmap, io.rect);
                    }
                    else if (renderObject.GetType() == typeof(TextObject))
                    {
                        TextObject to = (TextObject)renderObject;
                        //Checks for variables in text property
                        if (to.text == "%ACC%")
                        {
                            float f_acc = float.Parse(accBox.Text);
                            to.text = f_acc.ToString("0.00") + "%";
                            to.text = to.text.Replace(',', '.');
                        }
                        else if (to.text == "%SR%")
                        {
                            to.text = starBox.Text + "%";
                            to.text = to.text.Replace(',', '.');
                        }

                        SolidBrush brush = new SolidBrush(to.color);
                        g.DrawString(to.text, font, brush,to.position);
                    }
                    else if (renderObject.GetType() == typeof(RectangleObject))
                    {
                        RectangleObject ro = (RectangleObject)renderObject;

                        SolidBrush brush = new SolidBrush(ro.color);
                        g.FillRectangle(brush, ro.rect);
                    }
                }

            }
        }

        private Bitmap ColorTint (Bitmap src, Color tint)
        {
            BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[data.Stride * data.Height];

            float r, g, b;
            for (int i = 0; (i + 4) < pixelBuffer.Length; i += 4) {
                b = pixelBuffer[i]   + (255 - pixelBuffer[i])   * ((float)tint.B / 255);
                g = pixelBuffer[i+1] + (255 - pixelBuffer[i+1]) * ((float)tint.G / 255);
                r = pixelBuffer[i+2] + (255 - pixelBuffer[i+2]) * ((float)tint.R / 255);

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
                        string[] data = line.Split(':');
                        textObject.text = data[1].Substring(1, data[1].Length - 1);
                    }
                    else if (noSpaces.StartsWith("position:"))
                    {
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

                        string[] split = data[1].Split(',');

                        int x = int.Parse(split[0]);
                        int y = int.Parse(split[1]);

                        textObject.position = new Point(x, y);
                    }
                    else if (noSpaces.StartsWith("color:"))
                    {
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

                        string[] split = data[1].Split(',');

                        int r = int.Parse(split[0]);
                        int g = int.Parse(split[1]);
                        int b = int.Parse(split[2]);
                        int a = int.Parse(split[3]);

                        textObject.color = Color.FromArgb(a, r, g, b);
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
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

                        string[] split = data[1].Split(',');

                        int x = int.Parse(split[0]);
                        int y = int.Parse(split[1]);
                        int w = split[2] == " %MAX%" ? preview.Width : int.Parse(split[2]);
                        int h = split[2] == " %MAX%" ? preview.Height : int.Parse(split[3]);

                        rectangleObject.rect = new Rectangle(x, y, w, h);
                    }
                    else if (noSpaces.StartsWith("color:"))
                    {
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

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
                        string[] data = line.Split(':');
                        imageObject.path = data[1].Substring(1, data[1].Length - 1);
                    }
                    else if (noSpaces.StartsWith("rect:"))
                    {
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

                        string[] split = data[1].Split(',');

                        int x = int.Parse(split[0]);
                        int y = int.Parse(split[1]);
                        int w = split[2] == " %MAX%" ? preview.Width : int.Parse(split[2]);
                        int h = split[2] == " %MAX%" ? preview.Height : int.Parse(split[3]);

                        imageObject.rect = new Rectangle(x, y, w, h);
                    }
                    else if (noSpaces.StartsWith("color:"))
                    {
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

                        string[] split = data[1].Split(',');

                        int r = int.Parse(split[0]);
                        int g = int.Parse(split[1]);
                        int b = int.Parse(split[2]);
                        int a = int.Parse(split[3]);

                        imageObject.color = Color.FromArgb(a, r, g, b);
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
        }
    }
}
