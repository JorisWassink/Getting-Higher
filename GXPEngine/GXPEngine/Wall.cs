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
    public static bool WallTrigger = false;
    public Wall(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/Perfect.png", 1, 1)
    {
        wallhit = new WallHit();
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }

    void Update()
    {
        collider.isTrigger = WallTrigger;
    }
}

