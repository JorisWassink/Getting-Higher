using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


abstract class PickupBase : AnimationSprite
{
    protected PickupBase(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        Initialize(obj);
    }

    protected abstract void Initialize(TiledObject obj);

    public void Grab()
    {
        this.LateDestroy();
    }
}

