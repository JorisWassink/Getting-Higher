using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
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
    Vec2 _position;
    Ui ui = null;
    Sound leftNoise;
    Sound rightNoise;
    SoundChannel leftChannel;
    SoundChannel rightChannel;
    RotatingSpaceship _mygame;
    Collider oldCollider;
    AnimationSprite visual;
    float _speed;
    float falling;
    float maxVel = 15;
    float gravity = 0.25f;
    float tank = 500;
    public float fuel = 500;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool canCollide = true;
    public bool pInput = true;
    public float pScore;
    public bool isBoosting = false;
    int boostCount;
    int _lives;
    int timeHit = 0;
    int coolDownTime = 1000;

    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/spaceship.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        _mygame = (RotatingSpaceship)game;

        oldCollider = _collider;

        
        _position = new Vec2(x, y);
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scaleY = .15f;
        scaleX = .3f;
        scale = .5f;
        _position.x = game.width / 2;

        _speed = 0.7f;
        _lives = 2;
        pScore = position.y;

        rightNoise = new Sound("Assets/Jetpack_middle_left.mp3", true, false);
        leftNoise = new Sound("Assets/Jetpack_middle_right.WAV", true, false);

        rightChannel = (SoundChannel)leftNoise.Play();
        leftChannel = (SoundChannel)rightNoise.Play(); 
    }
    void Update()
    {
        if (ui == null)
        {
            ui = game.FindObjectOfType<Ui>();
        }
        Movement();
        UpdateScreenPosition();

        boostCount--;
        if (boostCount < 0)
        {
            _speed = 0.7f;
            isBoosting = false;
           // _collider = oldCollider;
        } else
        {
            velocity.y += 2;
        }
        if (velocity.y == 2)
        {
           falling = position.y;
            Console.WriteLine(falling);
        }
        if (position.y >= falling + 500)
        {
            Console.WriteLine("dead");
        }
        if (canCollide == false)
        {
            InvisFrames();
        }

    }


    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }

    public void Boost()
    {
        if (velocity.y > -78 && velocity.y < -0.1f)
        {
            velocity.y -= velocity.y + 50;
            isBoosting = true;
            boostCount = 30;
        }
    }


    void Movement()
    {
        SpeedCap();
        collisions();

        if (pInput) {
            PlayerInput();
        }

        if (_autoRotateLeft)
        {
            rotation -= 3;
        }
        if (_autoRotateRight)
        {
            rotation += 3;
        }

        if (pScore > position.y)
        {
            pScore = position.y;
        }
        if (pInput) {
            ui.SetScore(-((int)(pScore - 15291) / 3));
        }

        _position += velocity * _speed;
    }

    void SpeedCap()
    {
        if (!isBoosting)
        {
            if (velocity.y < 50)
            {
                velocity.y += gravity;
            }
            if (velocity.y <= -maxVel)
            {
                velocity.y = -maxVel;
            }
        }
    }


    //TODO: improve this code a lot this is a mess
    void PlayerInput()
    {
        HandleRotationInput();
        //HandleBoostInput();
        UpdateFuelUI();
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(Key.A) && Input.GetKey(Key.D) && fuel > 0)
        {
            HandleFullRotationInput();
        }
        else if (Input.GetKey(Key.A))
        {
            HandleLeftRotationInput();
        }
        else if (Input.GetKey(Key.D))
        {
            HandleRightRotationInput();
        }
        else
        {
            ResetRotation();
        }
    }

    void HandleFullRotationInput()
    {
        if (rotation <= -maxVel)
        {
            rotation += 1f;
        }
        else if (rotation >= maxVel)
        {
            rotation -= 1f;
        }

        //AdjustVelocityX();

    }

    void HandleLeftRotationInput()
    {
        if (fuel > 0)
        {
            BoostLeft();
            _autoRotateLeft = rotation > -50;
            leftChannel.IsPaused = !_autoRotateLeft;
        }
        else
        {
            ResetRotation();
            leftChannel.IsPaused = true;
        }
    }

    void HandleRightRotationInput()
    {
        if (fuel > 0)
        {
            BoostRight();
            _autoRotateRight = rotation < 50;
            rightChannel.IsPaused = !_autoRotateRight;
        }
        else
        {
            ResetRotation();
            rightChannel.IsPaused = true;
        }
    }

    void ResetRotation()
    {
        if (rotation < 0)
        {
            rotation += 2;
        }
        else if (rotation > 0)
        {
            rotation -= 2;
        }

        _autoRotateLeft = _autoRotateRight = false;
    }

    void BoostLeft()
    {
        fuel -= 1;
        ui.SetFuel((int)fuel);

        if (velocity.x > -maxVel)
        {
            velocity.x -= _speed * 1.5f;
        }

        if (velocity.y > -maxVel)
        {
            velocity.y -= _speed * 1.5f;
        }
    }

    void BoostRight()
    {
        fuel -= 1;
        ui.SetFuel((int)fuel);

        if (velocity.x < maxVel)
        {
            velocity.x += _speed * 1.5f;
        }

        if (velocity.y > -50)
        {
            velocity.y -= _speed * 1.5f;
        }
    }

    void UpdateFuelUI()
    {
        // Additional fuel UI update logic if needed
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
            if (collisions[i] is FuelCan)
            {
                ((FuelCan)collisions[i]).Grab();
                fuel += 250;
            }
            if (collisions[i] is Spikes && canCollide)
            {
                Spikes ouch = (Spikes)collisions[i];
                _lives -= 1;
                velocity = velocity * -1;
                canCollide = false;
                timeHit = Time.time;
                if (_lives == 0)
                {
                    Move(ouch.speed * ouch.direction, 0);
                    _mygame.dead = true;
                }
                }
            }
    }
    void InvisFrames()
    {
        if ((Time.time - timeHit) >= coolDownTime)
        {
            canCollide = true;
        }
    }

    public void pDead()
    {
        rightChannel.Stop();
        leftChannel.Stop();
        pInput = false;
        gravity = 0;
    }

}