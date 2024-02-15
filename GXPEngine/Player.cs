using System;
using System.Collections.Generic;
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
    Vec2 _position;
    float _speed;
    float gravity = 0.3f;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool _move = false;

    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        Initialize(obj);
        x = 864;
        y = 3489;

    }

    void Initialize(TiledObject obj)
    {
        _position = new Vec2(x, y);
        _speed = 0.5f;
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scale = .3f;
    }
    void Update()
    {
        UpdateScreenPosition();
        Movement();
        Console.WriteLine(rotation);
    }
    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }
    void Movement()
    {
        Collision colx = MoveUntilCollision(_position.x, 0);
        Collision coly = MoveUntilCollision(0, _position.y);
        if (colx != null && colx.normal.x < 0)
        {
            _position.x += 20;
        }
        if (coly != null)
        {
            velocity.y = 0;
        }
        //add gravity
        if (velocity.y < 50)
        {
            velocity.y += gravity;
        }

        if (Input.GetKey(Key.A))
        {
            //boost left
            velocity.x -= _speed;

            if (velocity.y > -50)
            {
                velocity.y -= _speed;
            }
            _autoRotateLeft = true;
            if (rotation <= -50)
            {
                _autoRotateLeft = false;
            }
        }
        else
        {
            _autoRotateLeft = false;
            _move = false;
        }
        if (Input.GetKey(Key.D))
        {
            //boost right
            _autoRotateRight = true;
            velocity.x += _speed;
            if (velocity.y > -50)
            {
                velocity.y -= _speed;
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
            _autoRotateRight = false;
            _move = false;
        }

        if (_autoRotateLeft)
        {
            rotation -= 1;
        }
        if (_autoRotateRight)
        {
            rotation += 1;
        }

        // move in current direction:
        /*if (_move)
        {
            if (Input.GetKey(Key.A) && Input.GetKey(Key.D))
            {
                Move(4, 0);
            }
            else Move(2, 0);
        }*/

        _position += velocity * _speed;
    }

}