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
    WallHit wallhit;
    public Wall(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/Perfect.png", 1, 1)
    {
        wallhit = new WallHit();
        Initialize(obj);
        
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
        wallhit.SetXY(this.x, this.y + 50);
        AddChild(wallhit);
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
        SetOrigin(width/2, height/2);
    }

    void Update()
    {
       // Console.WriteLine("wallhit:" + x + ' ' + y);
    }
}

