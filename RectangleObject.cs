using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class RectangleObject : RenderObject
    {
        private Rectangle _rect;
        private Color _color;

        public Rectangle rect
        {
            get
            {
                return _rect;
            }
        }
        public Color color
        {
            get
            {
                return _color;
            }
        }

        //Constructor
        public RectangleObject(Rectangle rect, Color color)
        {
            this._rect = rect;
            this._color = color;
        }
    }
}
