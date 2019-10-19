using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
