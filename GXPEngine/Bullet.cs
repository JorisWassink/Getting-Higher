using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

class Bullet : Sprite
{
    Vec2 position;
    Vec2 direction;
    EasyDraw Debug;
    private static float speed = 5f;
    public Bullet(Vec2 _distance, float X, float Y) : base("Assets/Spikeball.png")
    {
        SetOrigin(width / 2, height / 2);
        direction = _distance;
        collider.isTrigger = true;
        position = new Vec2(X, Y);
        direction.Normalize();
        Console.WriteLine(direction);
    }
    void Update()
    {
        Move();
        UpdateScreenPosition();
    }
    void Move()
    {
        /*float rotationInRad = rotation*(Mathf.PI/180);
        float speedX = speed * Mathf.Cos(rotationInRad);
        float speedY = speed * Mathf.Sin(-rotationInRad);
        position.x += speedX;
        position.y += speedY;*/
        position += direction * speed;

    }
    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
        
    }
}