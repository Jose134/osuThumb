using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class TextObject : RenderObject
    {
        public string text { get; private set; }
        public string suffix { get; private set; }
        public int fontSize { get; private set; }

        //Constructor
        public TextObject(
            string text,
            Color color,
            float x = 0,
            float y = 0,
            PositionType positionType = PositionType.Pixel,
            string suffix = "",
            int fontSize = 24
        )
            : base(positionType, x, y, color)
        {
            this.text = text;
            this.suffix = suffix;
            this.fontSize = fontSize;
        }

        public override void Render (ref Graphics graphics)
        {
            throw new NotImplementedException();

            /*

            //Checks for variables in text property
            string save = text;
            string variableName = null;
            if (Helper.IsCustomVariable(text, out variableName))
            {
                text = Helper.ReadVariable(variableName);
            }
            text = save;

            SolidBrush brush = new SolidBrush(color);
            Font font = new Font(layoutFont.)
            graphics.DrawString(text + suffix, );


                
            

            text += to.suffix;
            SolidBrush brush = new SolidBrush(to.color);
            Font font = new Font(layoutFont.FontFamily, to.textSize == -1 ? layoutFont.Size : to.textSize);

            int x = (int)to.position.Y;
            int y = (int)to.position.X;

            if (to.positionType == MeasureType.canvasmult)
            {
                x = (int)(to.position.X * bmp.Width);
                y = (int)(to.position.Y * bmp.Height);
            }

            Point position = new Point(x, y);
            g.DrawString(text, font, brush, position);

            */
        }
    }
}
