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
    public Wall(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/Perfect.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        GameObject[] collisions = GetCollisions();
        Console.WriteLine(collisions.Length);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Player)
            {
                if (((Player)collisions[i]).isBoosting)
                {
                    Destroy();
                }
                
            }
        }
    }
}

