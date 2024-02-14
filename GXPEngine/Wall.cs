using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


class Wall : AnimationSprite
{
    int obwidth;
    int obheight;

    public Wall(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
    }
}
