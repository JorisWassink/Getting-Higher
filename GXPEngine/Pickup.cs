using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


class Pickup : AnimationSprite
{
    public Pickup(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/jerrycan.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
    }
    public void Grab()
    {
        this.LateDestroy();
    }
}
