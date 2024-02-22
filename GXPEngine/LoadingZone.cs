using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
class LoadingZone : AnimationSprite
{
    public bool playerCollides;
    LevelManager thisManager;
    public LoadingZone(float xPos, float yPos, int oWidth, int oHeight, LevelManager manager) : base("Assets/Spikeball.png", 1, 1)
    {
        x = xPos;
        y = yPos;

        width = oWidth;
        height = oHeight;

        thisManager = manager;

        collider.isTrigger = true;
    }

    void Update()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Player)
            { 
                thisManager.LoadLevelNow();
                Destroy();
            } else
            {
                playerCollides = false;
            }
        }
    }
}