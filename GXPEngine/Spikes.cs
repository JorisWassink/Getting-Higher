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
    float startX;


    public Spikes(String fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spritesheetMoth.png", 3, 2)
    {
        Initialize(obj);
        
    }

    void Initialize(TiledObject obj)
    {
        SetOrigin(width/2, height/2);
        collider.isTrigger = true;
        startX = obj.X;
        //alpha = 0;
        if (startX > game.width / 2) 
        {
            direction *= -1;
        }

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
                direction *= -1;
                scaleX *= -1;
            }
        }

        //TODO: flip it
        if (direction == 1)
        {
            rotation = 90;
        }
        else if (direction == -1)
        {
            rotation = 270;
        }

        x += speed * direction;
    }
}
