using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class ImageObject
    {
        private Image _image;
        private string _path;
        private Rectangle _rect;
        private Color _color;

        public Image image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public string path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
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
        public ImageObject ()
        {
            this._image = null;
            this._path = "";
            this._rect = new Rectangle(-1, -1, -1, -1);
            this._color = Color.FromArgb(0, 0, 0, 0);
        }
        public ImageObject(string path, Rectangle rect, Color color)
        {
            this._path = path;
            this._rect = rect;
            this._color = color;

            LoadImage();
        }

        public void LoadImage ()
        {
            _image = Bitmap.FromFile(_path);
        }
    }
}
