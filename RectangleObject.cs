using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class RectangleObject : RenderObject
    {
        public SizeType sizeType { get; private set; }
        public float width       { get; private set; }
        public float height      { get; private set; }

        //Constructor
        public RectangleObject(
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
            this.sizeType = sizeType;
            this.width = width;
            this.height = height;
        }

        //Renders the object onto the graphics
        public override void Render (ref Graphics graphics)
        {
            SolidBrush b = new SolidBrush(color);

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
                    /*Rectangle objects don't implement 
                    this type so we operate as pixel*/
                    w = (int)Math.Floor(this.width);
                    h = (int)Math.Floor(this.height);
                    break;
                    
            }

            graphics.FillRectangle(b, x, y, w, h);
        }
    }

}
