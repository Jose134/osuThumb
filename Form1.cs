using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace osuThumb
{
    public partial class MainForm : Form
    {
        private string appdataPath = "";
        private string osuFolder = "";
        private string thumbFolder = "";

        private List<object> renderObjects;
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
            /*
            string bgPath = thumbFolder + @"\" + idBox.Text + "l.jpg";
            if (!File.Exists(bgPath))
            {
                MessageBox.Show("ERROR: Couldn't find background image, is id correct?", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Image bgImg = Bitmap.FromFile(bgPath);

            Graphics g = preview.CreateGraphics();
            g.DrawImage(bgImg, new RectangleF(0, 15, preview.Width, preview.Height - 15));

            SolidBrush semiBlackBursh = new SolidBrush(Color.FromArgb(191, 0, 0, 0));
            g.FillRectangle(semiBlackBursh, new Rectangle(0, 0, preview.Width, preview.Height));

            SolidBrush fontBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

            Font test = new Font(font.FontFamily, 70);

            float f_acc = float.Parse(accBox.Text);
            string acc = f_acc.ToString("0.00");
            g.DrawString(acc + "%", font, fontBrush, new PointF(30, 30));

            g.DrawString(starBox.Text + "*", test, fontBrush, new PointF());
            */

            //Renders each object in the list
            foreach (object renderObject in renderObjects)
            {
                if (renderObject.GetType() == typeof(ImageObject))
                {
                    ImageObject io = (ImageObject)renderObject;
                }
                else if (renderObject.GetType() == typeof(TextObject))
                {
                    TextObject to = (TextObject)renderObject;
                }
                else if (renderObject.GetType() == typeof(RectangleObject))
                {
                    RectangleObject ro = (RectangleObject)renderObject;
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "LAYOUT|*.layout";
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
                string noSpaces = line.Replace(@"\s", "");

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
                        int w = int.Parse(split[2]);
                        int h = int.Parse(split[3]);

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

                        imageObject.color = Color.FromArgb(a, r, g, b);
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
                        int w = int.Parse(split[2]);
                        int h = int.Parse(split[3]);

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
