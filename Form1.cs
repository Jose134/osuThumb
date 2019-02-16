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
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;

namespace osuThumb
{
    public partial class MainForm : Form
    {
        //Folder variables
        private string appdataPath = "";
        private string osuFolder = "";
        private string thumbFolder = "";
        private string layoutPath = "";

        //Drawing variables
        private List<object> renderObjects = new List<object>();
        private Font layoutFont = new Font("Arial", 24);

        private Bitmap render = new Bitmap(480, 360);

        public MainForm()
        {
            InitializeComponent();
        }

        //Called when the form is loaded, does some initial setup
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

            LoadDefault();
        }

        //Loads the layout in the default file
        private void LoadDefault ()
        {
            if (!File.Exists("default.cfg"))
            {
                MessageBox.Show("Error: default.cfg not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (StreamReader sr = new StreamReader("default.cfg"))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("layout"))
                        {
                            string[] data = line.Split('=');
                            layoutPath = data[1].Substring(1);
                        }
                    }
                }

                if (!File.Exists(layoutPath))
                {
                    MessageBox.Show("ERROR: the layout file in default.cfg wasn't found, maybe it was moved or deleted?", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LoadLayout(layoutPath);
                }
            }
        }

        //Saves current layout as default in the cfg file
        private void SaveToDefault ()
        {
            if (!File.Exists(layoutPath))
            {
                MessageBox.Show("ERROR: No .layout file loaded", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists("default.cfg"))
            {
                var file = File.Create("default.cfg");
                file.Close();
            }

            using (StreamWriter sw = new StreamWriter("default.cfg")) {
                sw.WriteLine("layout = " + layoutPath);
            }
        }

        //Calls the Save function when the button is pressed
        private void defaultButton_Click(object sender, EventArgs e)
        {
            SaveToDefault();
        }

        //Exports the image
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("export"))
            {
                Directory.CreateDirectory("export");
            }

            int number = 0;
            string filename = "";
            do
            {
                filename = "export/" + idBox.Text + "_thumb + _" + number + ".jpg";
                number++;
            } while (File.Exists(filename));

            render.Save(filename, ImageFormat.Jpeg);
        }

        //Changes the default font
        private void fontButton_Click(object sender, EventArgs e)
        {
            //Sets font using a FontDialog
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                layoutFont = fontDialog.Font;
            }
        }

        #region Thumbnail Generation

        //THUMBNAIL GENERATOR
        private void generateButton_Click(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(render.Width, render.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
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
                            if ((io.path[0] == '%') && (io.path[io.path.Length - 1] == '%'))
                            {
                                string variableName = io.path.Substring(1, io.path.Length - 2);

                                if (variableName == "BG")
                                {
                                    io.path = thumbFolder + @"\" + idBox.Text + "l.jpg";
                                }
                                else
                                {
                                    foreach (Control c in propertiesPanel.Controls)
                                    {
                                        if (c.GetType() == typeof(TextBox))
                                        {
                                            if (c.Name == "customVariableBox_" + variableName)
                                            {
                                                io.path = "res/" + variableName + "/" + c.Text;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            io.LoadImage();
                            io.path = save;

                            Bitmap bitmap = ColorTint((Bitmap)io.image, io.color);

                            Rectangle rect = new Rectangle(
                                (int)(io.rect.X * (io.canvasRelative ? bmp.Width : 1)),
                                (int)(io.rect.Y * (io.canvasRelative ? bmp.Width : 1)),
                                (int)(io.rect.Width * (io.canvasRelative ? bmp.Width : bitmap.Width)),
                                (int)(io.rect.Height * (io.canvasRelative ? bmp.Width : bitmap.Height))
                            );

                            g.DrawImage(bitmap, rect);
                        }
                        else if (renderObject.GetType() == typeof(TextObject))
                        {
                            TextObject to = (TextObject)renderObject;
                            string text = to.text;

                            //Checks for custom text variables
                            if ((to.text[0] == '%') && (to.text[to.text.Length - 1] == '%'))
                            {
                                string variableName = to.text.Substring(1, to.text.Length - 2);

                                foreach (Control c in propertiesPanel.Controls)
                                {
                                    if (c.GetType() == typeof(TextBox))
                                    {
                                        if (c.Name == "customVariableBox_" + variableName)
                                        {
                                            text = c.Text;
                                        }
                                    }
                                }
                            }

                            SolidBrush brush = new SolidBrush(to.color);
                            Font font = new Font(layoutFont.FontFamily, to.textSize == -1 ? layoutFont.Size : to.textSize);
                            Point position = new Point(
                                (int)(to.position.X * bmp.Width),
                                (int)(to.position.Y * bmp.Height)
                            );
                            g.DrawString(text, font, brush, position);
                        }
                        else if (renderObject.GetType() == typeof(RectangleObject))
                        {
                            RectangleObject ro = (RectangleObject)renderObject;

                            SolidBrush brush = new SolidBrush(ro.color);
                            Rectangle rect = new Rectangle(
                                (int)(ro.rect.X * (ro.canvasRelative ? bmp.Width : 1)),
                                (int)(ro.rect.Y * (ro.canvasRelative ? bmp.Height : 1)),
                                (int)(ro.rect.Width * (ro.canvasRelative ? bmp.Width : 1)),
                                (int)(ro.rect.Height * (ro.canvasRelative ? bmp.Height : 1))
                            );
                            g.FillRectangle(brush, rect);
                        }
                    }
                }

                render = (Bitmap)bmp.Clone();
                render.Save("thumb.bmp");
            }

            using (Graphics g = preview.CreateGraphics())
            {
                g.DrawImage(render, 0, 0, preview.Width, preview.Height);
            }
        }

        //Creates a tinted Bitmap
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

        #endregion

        #region Layout

        //LAYOUT LOADER
        private void loadButton_Click(object sender, EventArgs e)
        {
            //Creates a OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Layout file|*.layout";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                LoadLayout(filePath);
            }

        }

        //Reads a layout file adding elements to the renderObjects List
        private void LoadLayout (string filePath)
        {
            if (!File.Exists(filePath)) { return; }

            propertiesPanel.Controls.Clear();

            string current = "";

            TextObject textObject = null;
            RectangleObject rectangleObject = null;
            ImageObject imageObject = null;
            renderObjects.Clear();

            int customPropertyCount = 0;

            StreamReader sr = new StreamReader(filePath);

            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line == string.Empty) { continue; }

                string noSpaces = line.Replace(" ", "");

                //Looks for object start
                if (line[0] == '{')
                {
                    string[] data = line.Split(' ');

                    if (data[1] == "text")           { current = "text"; textObject = new TextObject();                }
                    else if (data[1] == "rectangle") { current = "rectangle"; rectangleObject = new RectangleObject(); }
                    else if (data[1] == "image")     { current = "image"; imageObject = new ImageObject();             }
                    else if (data[1] == "general")   { current = "general";                                            }
                }

                if (current == "general")
                {
                    //Looks for object properties
                    if (noSpaces.StartsWith("font-family:"))
                    {
                        string[] data = line.Split(':');
                        layoutFont = new Font(data[1].Substring(1), layoutFont.Size);
                    }
                    else if (noSpaces.StartsWith("font-size:"))
                    {
                        string[] data = line.Split(':');
                        layoutFont = new Font(layoutFont.FontFamily, int.Parse(data[1]));
                    }
                    else if (noSpaces.StartsWith("size"))
                    {
                        string[] data = line.Split(':');
                        data[1] = data[1].Substring(2, data[1].Length - 3);

                        string[] split = data[1].Split(',');

                        int width = int.Parse(split[0]);
                        int height = int.Parse(split[1]);

                        render = new Bitmap(width, height);
                    }
                }
                else if (current == "text")
                {
                    //Looks for object properties
                    if (noSpaces.StartsWith("text:"))
                    {
                        string[] data = noSpaces.Split(':');
                        textObject.text = data[1];

                        //Variable Generator
                        if ((textObject.text[0] == '%') && (textObject.text[textObject.text.Length - 1] == '%'))
                        {
                            string variableName = textObject.text.Substring(1, textObject.text.Length - 2);

                            Label variableLabel = new System.Windows.Forms.Label();
                            variableLabel.Name = "customVariableLabel_" + variableName;
                            variableLabel.Size = new System.Drawing.Size(variableName.Length * 10, 13);
                            variableLabel.Location = new System.Drawing.Point(80 - variableLabel.Size.Width, 25 * (customPropertyCount + 1) + 5);
                            variableLabel.TextAlign = ContentAlignment.MiddleRight;
                            variableLabel.Text = variableName;

                            TextBox variableBox = new System.Windows.Forms.TextBox();
                            variableBox.Name = "customVariableBox_" + variableName;
                            variableBox.Location = new System.Drawing.Point(100, 25 * (customPropertyCount + 1));
                            variableBox.Size = new System.Drawing.Size(180, 20);
                            variableBox.TabIndex = 13;

                            this.propertiesPanel.Controls.Add(variableLabel);
                            this.propertiesPanel.Controls.Add(variableBox);

                            customPropertyCount++;
                        }
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
                    else if (noSpaces.StartsWith("canvas-size:"))
                    {
                        string[] data = noSpaces.Split(':');
                        bool canvasRelative = true;
                        if (data[1] == "false")
                        {
                            canvasRelative = true;
                        }
                        rectangleObject.canvasRelative = canvasRelative;
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

                        //Variable Generator
                        if ((imageObject.path[0] == '%') && (imageObject.path[imageObject.path.Length - 1] == '%'))
                        {
                            string variableName = imageObject.path.Substring(1, imageObject.path.Length - 2);
                            
                            //Skips the BG variable since it's a special one
                            if (variableName != "BG")
                            {
                                Label variableLabel = new System.Windows.Forms.Label();
                                variableLabel.Name = "customVariableLabel_" + variableName;
                                variableLabel.Size = new System.Drawing.Size(variableName.Length * 10, 13);
                                variableLabel.Location = new System.Drawing.Point(80 - variableLabel.Size.Width, 25 * (customPropertyCount + 1) + 5);
                                variableLabel.TextAlign = ContentAlignment.MiddleRight;
                                variableLabel.Text = variableName;

                                TextBox variableBox = new System.Windows.Forms.TextBox();
                                variableBox.Name = "customVariableBox_" + variableName;
                                variableBox.Location = new System.Drawing.Point(100, 25 * (customPropertyCount + 1));
                                variableBox.Size = new System.Drawing.Size(180, 20);
                                variableBox.TabIndex = 13;

                                this.propertiesPanel.Controls.Add(variableLabel);
                                this.propertiesPanel.Controls.Add(variableBox);

                                customPropertyCount++;

                            }
                        }
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
                        bool canvasRelative = false;
                        if (data[1] == "true")
                        {
                            canvasRelative = true;
                        }
                        imageObject.canvasRelative = canvasRelative;
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

            layoutPath = filePath;
        }

        #endregion
    }
}