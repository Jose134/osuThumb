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

        private List<RenderObject> renderObjects;
        private Font font = new Font("Arial", 24);

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog.Font;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
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

                //Look for object start
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
