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
        private RectangleF _rect;
        private Color _color;
        private bool _canvasSize;

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
        public bool canvasSize
        {
            get
            {
                return _canvasSize;
            }
            set
            {
                _canvasSize = value;
            }
        }

        //Constructor
        public ImageObject ()
        {
            this._image = null;
            this._path = "";
            this._rect = new RectangleF(0, 0, 1, 1);
            this._color = Color.FromArgb(255, 255, 255, 255);
            this._canvasSize = false;
        }
        public ImageObject(string path, RectangleF rect, Color color, bool canvasSize)
        {
            this._path = path;
            this._rect = rect;
            this._color = color;
            this._canvasSize = canvasSize;
            this._canvasSize = canvasSize;
            this._canvasSize = canvasSize;
            this._canvasSize = canvasSize;

            LoadImage();
        }

        public void LoadImage ()
        {
            _image = Bitmap.FromFile(_path);
        }
    }
}
