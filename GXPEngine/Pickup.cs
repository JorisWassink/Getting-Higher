using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class PickUp : AnimationSprite
{
    protected PickUp(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize(obj);
    }

    public void Initialize(TiledObject obj)
    {
    }

public void Grab()
    {
        this.LateDestroy();
    }
}

/*public class Shield(string imageFile, int cols, int rows, TiledObject obj = null) : PickUp("Shield.png", 1, 1)
{


}*/