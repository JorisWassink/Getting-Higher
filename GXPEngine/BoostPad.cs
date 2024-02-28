using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;


class BoostPad : PickUpBase
{
    public BoostPad(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/jerrycan.png", 1, 1)
    {
    }

    protected override void Initialize(TiledObject obj)
    {
        collider.isTrigger = true;
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        
    }
}

