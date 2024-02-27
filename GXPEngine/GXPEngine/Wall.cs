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
    WallHit wallhit = new WallHit();
    public Wall(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/Perfect.png", 1, 1)
    {
        Initialize(obj);
        AddChild(wallhit);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }

    void Update()
    {
    }
}
public class WallHit : Sprite
{
    public WallHit() : base("Assets/Spikeball.png")
    {
        collider.isTrigger = true;
        /*alpha = 0;*/
        SetOrigin(width/2, height - 50);
    }
}

