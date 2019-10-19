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
        public SizeType sizeType { get; private set; }
        public float width       { get; private set; }
        public float height      { get; private set; }
        public string imagePath  { get; private set; }

        //Constructor
        public ImageObject(
            string imagePath,
            Color color,
            float x = 0,
            float y = 0,
            float width = 0,
            float height = 0,
            PositionType positionType = PositionType.Pixel,
            SizeType sizeType = SizeType.Pixel
        )
            : base(positionType, x, y, color)
        {
            this.imagePath = imagePath;
            this.sizeType = sizeType;
            this.width = width;
            this.height = height;
        }
        
        /*
        private bool LoadImage ()
        {
            bool success = true;

            try
            {
                _image = Bitmap.FromFile(_path);
            }
            catch (System.IO.FileNotFoundException e)
            {
                
                success = false;
            }

            return success;
        }
        */

        public override void Render (ref Graphics graphcis)
        {
            throw new NotImplementedException();
        }

    }
}
