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
            set
            {
                _rect = value;
            }
        }
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        //Constructor
        public RectangleObject ()
        {
            this._rect = new Rectangle(-1, -1, -1, -1);
            this._color = Color.FromArgb(0, 0, 0, 0);
        }
        public RectangleObject(Rectangle rect, Color color)
        {
            this._rect = rect;
            this._color = color;
        }
    }
}
