using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
class Spikes : AnimationSprite
{
    float speed = 8;
    int direction = 1;
    public Spikes(String fileName, int cols, int rows, TiledObject obj = null) : base("Assets/Spikeball.png", 1, 1)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }

    void Update()
    {
        MoveEnemies();
    }

    void MoveEnemies()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is EnemyTurn)
            {
                direction *= -1;
            }
        }
        x += speed * direction;
    }
}
