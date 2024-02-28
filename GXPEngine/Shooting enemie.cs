using System;
using System.Collections.Generic;
using System.Xml.Schema;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
class Shooter : AnimationSprite
{
    public Vec2 position;
    float bulletcount;
    Player player;
    Bullet bullet;
    int run = 1;
    public Shooter(String fileName, int cols, int rows, TiledObject obj = null) : base("Assets/Spikeball.png", 1, 1)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
        x = obj.X; 
        y = obj.Y;
        position = new Vec2(x, y);
    }

    void Update()
    {
        player = game.FindObjectOfType<Player>();
        Vec2 distance = player.position - this.position;
        float distance2 = distance.Length();
        if (distance2 < 100)
        {
            Shoot();
            Console.WriteLine(bulletcount);
            Console.WriteLine();
        }
        UpdateScreenPosition();

    }
    void Shoot()
    {
        if (player != null)
        {
            Vec2 distance = player.position - this.position;
            /*float rotationShooting = Mathf.Atan2(player.position.x, player.position.y);*/
            LateAddChild(new Bullet(distance, 0, 0));
            bulletcount++;
        }
    }
    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

}