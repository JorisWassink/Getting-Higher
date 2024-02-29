using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
class Spikes : AnimationSprite
{
    public float speed = 8;
    public int direction = 1;
    int animationDelay = 10;
    int frame = 1;


    public Spikes(String fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spritesheetMoth.png", 3, 2)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width/2, height/2);
        collider.isTrigger = true;
        //alpha = 0;

    }

    void Update()
    {
        MoveEnemies();
        animationDelay--;
        if (animationDelay == 0)
        {
            if(frame == 5)
            {
                frame = 0;
            } else
            {
                frame++;
            }
            animationDelay = 10;
        }
       SetFrame(frame);
    }

    void MoveEnemies()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is EnemyTurn)
            {
                //direction *= -1;
                scaleX *= -1;
               
            }
        }
      
        //TODO: flip it
        x += speed * direction;

    }
}
