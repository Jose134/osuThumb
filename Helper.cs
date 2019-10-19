using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace osuThumb
{
    static class Helper
    {
        public static MainForm form;
        public static string appdataPath = "";
        public static string osuFolder = "";
        public static string songsPath = "";
        public static string bgFilePath = "";
        public static string layoutPath = "";

        //Checks if a given string is a custom variable and returns the name if it is
        public static bool IsCustomVariable (string str, out string name)
        {
            name = null;
            bool isVariable = str[0] == '%' && str[str.Length - 1] == '%';

            if (isVariable)
            {
                name = str.Substring(1, str.Length - 2);
            }

            return isVariable;
        }

        //Gets the value of a custom variable by checking the TextBoxes in the main form
        public static string ReadVariable (string variable)
        {
            foreach (TextBox tb in form.Controls)
            {
                if (tb.Name == "customVariableBox_" + variable)
                {
                    return tb.Text;
                }
            }
            return null;
        }
        
        //Creates an error message box displaying the given message
        public static void ShowError (string message)
        {
            MessageBox.Show("ERROR: " + message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Creates a tinted Bitmap
        public static Bitmap ColorTint(Bitmap src, Color tint)
        {
            BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[data.Stride * data.Height];
            Marshal.Copy(data.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            float r, g, b;
            for (int i = 0; (i + 4) < pixelBuffer.Length; i += 4)
            {
                b = pixelBuffer[i] * ((float)tint.B / 255);
                g = pixelBuffer[i + 1] * ((float)tint.G / 255);
                r = pixelBuffer[i + 2] * ((float)tint.R / 255);

                if (b > 255) { b = 255; }
                if (g > 255) { g = 255; }
                if (r > 255) { r = 255; }

                pixelBuffer[i] = (byte)b;
                pixelBuffer[i + 1] = (byte)g;
                pixelBuffer[i + 2] = (byte)r;
            }

            Bitmap result = new Bitmap(src.Width, src.Height);
            BitmapData resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            result.UnlockBits(resultData);

            return result;
        }
    }
}
