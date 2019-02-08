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
        private string _text;
        private Point _position;
        private Color _color;

        public string text
        {
            get
            {
                return _text;
            }
        }
        public Point position
        {
            get
            {
                return _position;
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
        public TextObject(string text, Point position, Color color)
        {
            this._text = text;
            this._position = position;
            this._color = color;
        }
    }
}
