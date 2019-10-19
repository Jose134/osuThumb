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

        public override void Render (ref Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
