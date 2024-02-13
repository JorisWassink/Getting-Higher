using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

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
    float gravity = 1;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool _move = false;
    public Player(Vec2 pPosition, string fileName, int cols, int rows, float pSpeed = 2) : base(fileName, 1, 1)
    {
        _position = pPosition;
        _speed = pSpeed;
        SetOrigin(width / 2, height / 2);
        rotation = 270;

    }
    void Update()
    {
        UpdateScreenPosition();
        Movement();
    }
    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }
    void Movement()
    {
        velocity.x = 0;
        velocity.y = 0;

        velocity.y += gravity;

        if (Input.GetKey(Key.A))
        {
            velocity.x -= _speed;
            velocity.y -= _speed;
            _autoRotateLeft = true;
            if (rotation <= 190)
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
            _autoRotateRight = true;
            velocity.x += _speed;
            velocity.y -= _speed;

            if (rotation >= 350)
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
            rotation -= 1.5f;
        }
        if (_autoRotateRight)
        {
            rotation += 1.5f;
        }

        // move in current direction:
        if (_move)
        {
            if (Input.GetKey(Key.A) && Input.GetKey(Key.D))
            {
                Move(4, 0);
            }
            else Move(2, 0);
        }
        _position += velocity * _speed;
    }

}
