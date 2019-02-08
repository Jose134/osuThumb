using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class ImageObject : RenderObject
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
        }
        public string path
        {
            get
            {
                return _path;
            }
        }
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
        public ImageObject(string path, Rectangle rect, Color color)
        {
            this._path = path;
            this._rect = rect;
            this._color = color;
            
            this._image = Bitmap.FromFile(_path);
        }
    }
}
