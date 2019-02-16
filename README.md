# osu!Thumbnail Generator

This is a program that will generate thumbnails for my osu! youtube videos (lately I've been too lazy to make them manually)


## The .layout file format
It's just a text file, the syntax for these files is pretty simple
 
you create an object like this

{ ObjectType\
  property1: value\
  property2: value\
  ...\
  propertyN: value\
}


Objects will be drawn in order so the one at the bottom of the file will be the last one drawn

There are currently 3 Object types:\
**image**\
**text**\
**rectangle**



**Properties:**
Properties have a default value so you don't need to write every property for every object.

This is the list of properties for each Object type:

**image:**
  - path: The path where the image file is stored. 
    default: NONE
  - canvas-relative: Can be either true or false, this will make the rect relative to the canvas instead of to the image file.
    default: false
  - rect: Rect where the image will be drawn, if canvas-relative is true the values will be multiplied by canvas' width and height.
    default: (0, 0, 1, 1)
  - color: The image gets tinted with this color. The alpha channel is ignored but must be written.
    default: (255, 255, 255, 255)
    
**text:**
  - text: The text that will be rendered.
    default: NONE
  - position: Point where the text will be drawn.
    default: (0, 0)
  - color: Text's color.
    default: (255, 255, 255, 255)
  - font-size: Font's size.
    default: (taken from default font)
    
**rectangle:**
  - canvas-relative: Can be either true or false, this will make the rect relative to the canvas.
    default: true
  - rect: Rectangle to draw, if canvas-relative is true the values will be multiplied by canvas' width and height.
    default: (0, 0, 1, 1)
  - color: The color of the rectangle.
    default: (255, 255, 255, 255)
    

   
