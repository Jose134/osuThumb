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
        
        private Image LoadImage ()
        {
            Image im = null;

            try
            {
                im = Bitmap.FromFile(imagePath);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Helper.ShowError("couldn't load image " + e.FileName);
            }

            return im;
        }

        public override void Render (ref Graphics graphics)
        {
            //Checks for variables in path property
            string save = imagePath;
            string variableName = null;
            if (Helper.IsCustomVariable(imagePath, out variableName))
            {
                if (variableName == "BG")
                {
                    imagePath = Helper.bgFilePath;
                }
                else
                {
                    imagePath = Helper.ReadVariable(variableName);
                }
            }

            Image image = LoadImage();
            if (image != null)
            {
                Bitmap bitmap = Helper.ColorTint((Bitmap)image, color);

                int x = 0, y = 0, w = 0, h = 0;

                //Calculates position
                switch (positionType)
                {
                    case PositionType.Pixel:
                        x = (int)Math.Floor(this.x);
                        y = (int)Math.Floor(this.y);
                        break;
                    case PositionType.CanvasMult:
                        x = (int)Math.Floor(this.x * graphics.ClipBounds.Width);
                        y = (int)Math.Floor(this.y * graphics.ClipBounds.Height);
                        break;
                }

                //Calculates size
                switch (sizeType)
                {
                    case SizeType.Pixel:
                        w = (int)Math.Floor(this.width);
                        h = (int)Math.Floor(this.height);
                        break;
                    case SizeType.CanvasMult:
                        w = (int)Math.Floor(this.width * graphics.ClipBounds.Width);
                        h = (int)Math.Floor(this.height * graphics.ClipBounds.Height);
                        break;
                    case SizeType.SelfMult:
                        w = (int)Math.Floor(this.width * bitmap.Width);
                        h = (int)Math.Floor(this.height * bitmap.Height);
                        break;
                }

                Rectangle rect = new Rectangle(x, y, w, h);
                graphics.DrawImage(bitmap, rect);
            }

            imagePath = save;
        }

    }
}
