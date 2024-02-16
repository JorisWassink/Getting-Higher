using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class EnemyTurn : AnimationSprite
{
    public EnemyTurn(String fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        alpha = 0;
        collider.isTrigger = true;
    }
}