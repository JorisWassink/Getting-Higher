using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
class LoadingZone : AnimationSprite
{
   public LevelManager thisManager;
    public LoadingZone(float xPos, float yPos, int oWidth, int oHeight, LevelManager manager) : base("Assets/Spikeball.png", 1, 1)
    {
        x = xPos;
        y = yPos;

        width = oWidth;
        height = oHeight;

        thisManager = manager;

        collider.isTrigger = true;

        alpha = 0f;
    }

    void Update()
    {
    }
}