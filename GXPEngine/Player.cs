using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Player : AnimationSprite
{
    public Vec2 position
    {
        get
        {
            return _position;
        }
    }
    public Vec2 velocity;
    Ui ui = null;
    int tank = 1000;
    int fuel = 1000;
    Vec2 _position;
    float _speed;
    float maxVel = 10;
    float gravity = 0.5f;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool _move = false;

    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        Initialize(obj);
        Console.WriteLine(x + " " + y);
    }

    void Initialize(TiledObject obj)
    {
        _position = new Vec2(x, y);
        _speed = 0.5f;
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scaleY = .3f;
        scaleX = .4f;
        _position.x = game.width / 2;
        _position.y = 3300;
    }
    void Update()
    {
        if (ui == null) ui = game.FindObjectOfType<Ui>();
        Movement();
        UpdateScreenPosition();
        Console.WriteLine(velocity);
    }
    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;

    }
    void Movement()
    {
        if (velocity.y < 50)
        {
            velocity.y += gravity;
        }
        if (velocity.y <= -maxVel)
        {
            velocity.y = -maxVel;
        }
        collisions();

        //add gravity

        if (Input.GetKey(Key.A) && fuel > 0)
        {
            //boost left
            velocity.x -= _speed;
            fuel -= 1;
            ui.SetFuel(fuel);

            if (velocity.y > -50)
            {
                velocity.y -= _speed * 1.5f;
            }
            _autoRotateLeft = true;
            if (rotation <= -50)
            {
                _autoRotateLeft = false;
            }
        }
        else if (Input.GetKey(Key.D))
        {
            _autoRotateLeft = false;
        }
        else
        {
            if (rotation < 0)
            {
                rotation += 2;
            }
            _autoRotateLeft = false;
            _move = false;
        }
        if (Input.GetKey(Key.D) && fuel > 0)
        {
            //boost right
            fuel -= 1;
            ui.SetFuel(fuel);
            _autoRotateRight = true;
            velocity.x += _speed;
            if (velocity.y > -50)
            {
                velocity.y -= _speed * 1.5f;
            }

            if (rotation >= 50)
            {
                _autoRotateRight = false;
            }
        }
        else if (Input.GetKey(Key.A))
        {
            _autoRotateRight = false;
        }
        else
        {
            if (rotation > 0)
            {
                rotation -= 2;
            }

            _autoRotateRight = false;
            _move = false;
        }
        if (Input.GetKey(Key.A) && Input.GetKey(Key.D) && fuel > 0)
        {
            if (rotation > 0)
            {
                rotation -= 2;
            }
            else if (rotation < 0)
            {
                rotation += 2;
            }
            velocity.x += 0;
        }

        if (_autoRotateLeft)
        {
            rotation -= 5;
        }
        if (_autoRotateRight)
        {
            rotation += 5;
        }

        _position += velocity * _speed;
    }
    void collisions()
    {
        Collision colx = MoveUntilCollision(velocity.x, 0);
        Collision coly = MoveUntilCollision(0, velocity.y);
        if (coly != null)
        {
            if (coly.normal.y > 0)
            {
                velocity.y = 0;
                _position.y += _speed + 1;
            }
            if (coly.normal.y < 0)
            {
                velocity.y = 0;
                velocity.x = 0;
                rotation = 0;
                if (fuel < tank)
                {
                    fuel += 5;
                    if (fuel > tank) fuel = tank;
                    ui.SetFuel(fuel);
                }
            }
        }
        if (colx != null)
        {
            if (colx.normal.x > 0)
            {
                velocity.x = 0;
                _position.x += 1;
            }
            if (colx.normal.x < 0)
            {
                velocity.x = 0;
                _position.x -= 1;
            }
        }
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is Pickup)
            {
                ((Pickup)collisions[i]).Grab();
                fuel += 250;
            }
        }
    }

}