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
        private MeasureType _positionType;
        private MeasureType _sizeType;

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
        public MeasureType positionType
        {
            get
            {
                return _positionType;
            }
            set
            {
                _positionType = value;
            }
        }
        public MeasureType sizeType
        {
            get
            {
                return _sizeType;
            }
            set
            {
                _sizeType = value;
            }
        }

        //Constructor
        public ImageObject ()
        {
            this._image = null;
            this._path = "";
            this._rect = new RectangleF(0, 0, 1, 1);
            this._color = Color.FromArgb(255, 255, 255, 255);
            this._positionType = MeasureType.pixels;
            this._sizeType = MeasureType.pixels;
        }
        public ImageObject(string path, RectangleF rect, Color color)
        {
            this._path = path;
            this._rect = rect;
            this._color = color;
            this._positionType = MeasureType.pixels;
            this._sizeType = MeasureType.pixels;

            LoadImage();
        }

        public bool LoadImage ()
        {
            bool success = true;

            try
            {
                _image = Bitmap.FromFile(_path);
            }
            catch
            {
                success = false;
            }

            return success;
        }
    }
}
