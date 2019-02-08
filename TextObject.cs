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
        private PointF _position;
        private Color _color;
        private int _textSize;

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
        public PointF position
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
        public int textSize
        {
            get
            {
                return _textSize;
            }
            set
            {
                _textSize = value;
            }
        }

        //Constructor
        public TextObject ()
        {
            this._text = "";
            this._position = new PointF(0, 0);
            this._color = Color.FromArgb(255, 255, 255, 255);
            this.textSize = -1;
        }
        public TextObject(string text, PointF position, Color color, int textSize)
        {
            this._text = text;
            this._position = position;
            this._color = color;
            this.textSize = textSize;
        }
    }
}
