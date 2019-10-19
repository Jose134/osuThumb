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

            //Checks for variables in path property
            string save = imagePath;
            if ((imagePath[0] == '%') && (imagePath[imagePath.Length - 1] == '%'))
            {
                string variableName = path.Substring(1, path.Length - 2);

                if (variableName == "BG")
                {
                    path = bgFilePath;
                }
                else
                {
                    string varValue = MainForm.ReadVariable(variableName);
                }
            }

            if (LoadImage())
            {
                Bitmap bitmap = ColorTint((Bitmap)image, color);

                int x = (int)rect.X;
                int y = (int)rect.Y;
                int w = (int)rect.Width;
                int h = (int)rect.Height;

                //positType == MeasureType.pixels case omitted because x and y wouldn't need to change their value
                if (positType == MeasureType.canvasmult)
                {
                    x = (int)(rect.X * bmp.Width);
                    y = (int)(rect.Y * bmp.Height);
                }

                //sizeType == MeasureType.pixels case omitted because w and h wouldn't need to change their value
                if (sizeType == MeasureType.mult)
                {
                    w = (int)(rect.Width * bitmap.Width);
                    h = (int)(rect.Height * bitmap.Height);
                }
                else if (sizeType == MeasureType.canvasmult)
                {
                    w = (int)(rect.Width * bmp.Width);
                    h = (int)(rect.Height * bmp.Height);
                }

                Rectangle rect = new Rectangle(x, y, w, h);

                g.DrawImage(bitmap, rect);
            }
            path = save;
        }

    }
}
