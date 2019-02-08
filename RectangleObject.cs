using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class RectangleObject
    {
        private RectangleF _rect;
        private Color _color;

        public RectangleF rect
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
            this._rect = new RectangleF(0, 0, 1, 1);
            this._color = Color.FromArgb(255, 255, 0, 255);
        }
        public RectangleObject(RectangleF rect, Color color)
        {
            this._rect = rect;
            this._color = color;
        }
    }
}
