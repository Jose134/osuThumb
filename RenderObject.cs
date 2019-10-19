using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    enum PositionType
    {
        Pixel,
        CanvasMult
    };

    enum SizeType
    {
        Pixel,
        CanvasMult,
        SelfMult
    };

    abstract class RenderObject
    {
        public PositionType positionType { get; protected set; }
        public float x                   { get; protected set; }
        public float y                   { get; protected set; }
        public Color color               { get; protected set; }

        //Constructor
        public RenderObject (PositionType positionType, float x, float y, Color color)
        {
            this.positionType = positionType;
            this.x = x;
            this.y = y;
            this.color = color;
        }

        //Renders the object onto the graphcis
        public abstract void Render(ref Graphics g);
    }
}
