using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Player : AnimationSprite
{
    public Vec2 position
    {
        get
        {
            return _position;
        }
    }
    public Vec2 velocity;
    float tank = 500;
    float fuel = 500;
    Vec2 _position;
    RotatingSpaceship _mygame;
    float _speed;
    float maxVel = 25;
    float gravity = 0.5f;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    Ui ui = null;

    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        _mygame = (RotatingSpaceship)game;
        _position = new Vec2(x, y);
        _speed = 0.7f;
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scaleY = .15f;
        scaleX = .3f;
        _position.x = game.width / 2;
        _position.y = 14800;
    }
    void Update()
    {
        if (ui == null) ui = game.FindObjectOfType<Ui>();
        Movement();
        UpdateScreenPosition();
        //Console.WriteLine(rotation);
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

        if (Input.GetKey(Key.A) && Input.GetKey(Key.D) && fuel > 0)
        {
            if (rotation <= -25)
            {
                rotation += 1f;
            }
            else if (rotation >= 25)
            {
                rotation -= 1f;
            }
            if (velocity.x > 25)
            {
                velocity.x -= 1;
            }
            if (velocity.x < -25)
            {
                velocity.x += 1;
            }
        }

        if (Input.GetKey(Key.A) && fuel > 0)
        {
            //boost left
            if (velocity.x > -25) {
                velocity.x -= _speed * 1.5f;
            }
            fuel -= 1;
            ui.SetFuel((int)fuel);

            if (velocity.y > -25)
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
        }
        if (Input.GetKey(Key.D) && fuel > 0)
        {
            //boost right
            fuel -= 1;
            ui.SetFuel((int)fuel);
            _autoRotateRight = true;
            if (velocity.x < 25)
            {
                velocity.x += _speed * 1.5f; 
            }
            if (velocity.y > -50)
            {
                // Add velocity
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
        }



        if (_autoRotateLeft)
        {
            rotation -= 4;
        }
        if (_autoRotateRight)
        {
            rotation += 4;
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
                    ui.SetFuel((int)fuel);
                }
            }
        }
        if (colx != null)
        {
            if (colx.normal.x > 0)
            {
                velocity.x = 0;
                _position.x += 1;
                rotation = 0;
            }
            if (colx.normal.x < 0)
            {
                velocity.x = 0;
                _position.x -= 1;
                rotation = 0;
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
            if (collisions[i] is Spikes)
            {
                _mygame.dead = true;
            }
        }
    }

}