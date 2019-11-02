using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osuThumb
{
    class RenderLayout
    {
        //Singleton
        public static RenderLayout current
        {
            get
            {
                if (current == null)
                {
                    current = new RenderLayout();
                }
                return current;
            }
            set
            {
                current = value;
            }
        }

        public List<RenderObject> renderObjects { get; private set; }
        public int resWidth { get; private set; }
        public int resHeight { get; private set; }
        public Font font { get; private set; }

        //Defaults
        public RenderLayout()
        {
            this.renderObjects = new List<RenderObject>();
            this.resWidth = 1280;
            this.resHeight = 720;
            this.font = new Font("Arial", 24);
        }

        //Sets the resolution of the Layout
        public void SetResolution(int width, int height)
        {
            resWidth = width;
            resHeight = height;
        }

        //Sets the font of the layout (from a Font)
        public void SetFont(Font font)
        {
            this.font = font;
        }

        //Sets the font of the layout (from a FontFamily + a size)
        public void SetFont(FontFamily fontFamily, float size)
        {
            font = new Font(fontFamily, size);
        }

        //Adds a RenderObject to the list
        public void AddRenderObject(RenderObject renderObject)
        {
            renderObjects.Add(renderObject);
        }

        //Returns a Bitmap with all RenderObjects rendered
        public Bitmap Render()
        {
            Bitmap frame = new Bitmap(resWidth, resHeight);

            Graphics g = Graphics.FromImage(frame);
            g.Clear(Color.Black);

            //Renders each object in the list
            foreach (object renderObject in renderObjects)
            {
                RenderObject ro = (RenderObject)renderObject;
                ro.Render(ref g);
            }

            return frame;
        }

    }
}
