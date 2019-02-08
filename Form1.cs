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

            Font font = new Font("Arial", 24);
            SolidBrush fontBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
            
            float f_acc = float.Parse(accBox.Text);
            string acc = f_acc.ToString("0.00");

            g.DrawString(acc + "%", font, fontBrush, new PointF(30, 30));
        }
    }
}
