using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class TextObject
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
            set
            {
                _text = value;
            }
        }
        public Point position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
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
        public TextObject ()
        {
            this._text = "";
            this._position = new Point(-1, -1);
            this._color = Color.FromArgb(0, 0, 0, 0);
        }
        public TextObject(string text, Point position, Color color)
        {
            this._text = text;
            this._position = position;
            this._color = color;
        }
    }
}
