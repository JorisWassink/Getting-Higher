using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
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
    EasyDraw shield;
    /*Shield shield;*/
    //LevelManager thisManager;
    int shieldWidth;
    int shieldHeight;
    float shieldX;
    float shieldY;
    float _speed;
    float falling;
    float maxVel = 15;
    float gravity = 0.25f;
    float tank = 500;
    public float fuel = 500;
    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool canCollide = true;
    bool shieldOn = false;
    public bool pInput = true;
    public float pScore;
    public bool isBoosting = false;
    int boostCount;
    int _lives;
    int timeHit = 0;
    int coolDownTime = 1000;

    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/Player.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        _mygame = (RotatingSpaceship)game;

        oldCollider = _collider;

        y += 420;

        _position = new Vec2(x, y);
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scaleY = 10f;
        scaleX = .3f;
        scale = .5f;
        _position.x = game.width / 2;
        _speed = 0.7f;
        _lives = 1;
        pScore = position.y ;

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
        HandleBoosting();
        shieldX = position.x;
        shieldY = position.y;
        shieldWidth = this.width;
        shieldHeight = this.height;
        shield = new EasyDraw(1000, 1000, false);
        shield.Fill(255, 255, 255);
        shield.StrokeWeight(10);
        shield.Ellipse(shieldX, shieldY, 500, 500);
        shield.Fill(Color.Aqua);
        //shield.SetXY(shieldX, shieldY);
        if (shieldOn)
        {
            AddChild(shield);
            if(GetChildren().Count > 1)
            {
                GetChildren()[1].Destroy();
            }
            Console.WriteLine(GetChildren().Count);
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
            velocity.y -= /*velocity.y +*/ 30;
            isBoosting = true;
            boostCount = 30;
        }
    }

    void HandleBoosting()
    {
        boostCount--;
        if (boostCount < 0)
        {
            _speed = 0.7f;
            isBoosting = false;
            Wall.WallTrigger = false;
        }
        else
        {
            velocity.y += 2;
        }
        if (velocity.y == 2)
        {
            falling = position.y;
            Console.WriteLine(falling);
        }
        if (position.y >= falling + 700)
        {
            _mygame.dead = true;
            falling = position.y;

        }
        if (canCollide == false)
        {
            InvisFrames();
        }
    }

    void Movement()
    {
        SpeedCap();
        collisions();
        if (pInput)
        {
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
        if (pInput && ui != null)
        {
            ui.SetScore(-((int)(pScore) / 3) + 140);
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

    void PlayerInput()
    {
        HandleLeftThrust();
        HandleRightThrust();
        HandleStraightThrust();
    }

    void HandleLeftThrust()
    {
        if (Input.GetKey(Key.A) && fuel > 0)
        {
            //boost left
            if (velocity.x > -maxVel)
            {
                velocity.x -= _speed * 1.5f;
            }
            fuel -= 1;
            if(ui != null)
            ui.SetFuel((int)fuel);

            if (velocity.y > -maxVel)
            {
                velocity.y -= _speed * 1.5f;
            }
            _autoRotateLeft = true;
            if (rotation <= -50)
            {
                _autoRotateLeft = false;
            }

            leftChannel.IsPaused = false;
        }
        else if (Input.GetKey(Key.D))
        {
            _autoRotateLeft = false;
            leftChannel.IsPaused = true;
        }
        else
        {
            leftChannel.IsPaused = true;
            if (rotation < 0)
            {
                rotation += 2;
            }
            _autoRotateLeft = false;
        }
    }

    void HandleRightThrust()
    {
        if (Input.GetKey(Key.D) && fuel > 0)
        {
            //boost right
            fuel -= 1;
            if(ui != null)
            ui.SetFuel((int)fuel);
            _autoRotateRight = true;
            if (velocity.x < maxVel)
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
            rightChannel.IsPaused = false;
        }
        else if (Input.GetKey(Key.A))
        {
            _autoRotateRight = false;
            rightChannel.IsPaused = true;
        }
        else
        {
            rightChannel.IsPaused = true;
            if (rotation > 0)
            {
                rotation -= 2;
            }

            _autoRotateRight = false;
        }
    }

    //make sure that if the player goes straight up that the rotation slowly turns to normal
    void HandleStraightThrust()
    {
        if (Input.GetKey(Key.A) && Input.GetKey(Key.D) && fuel > 0)
        {
            if (rotation <= -maxVel)
            {
                rotation += 1f;
            }
            else if (rotation >= maxVel)
            {
                rotation -= 1f;
            }
            if (velocity.x > maxVel)
            {
                velocity.x -= 1;
            }
            if (velocity.x < -maxVel)
            {
                velocity.x += 1;
            }
        }
    }

    void collisions()
    {
        HandleWalls();

        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] is FuelCan)
            {
                ((FuelCan)collisions[i]).Grab();
                fuel += 250;
            }
            if (collisions[i] is ShieldPickUp)
            {
                ((ShieldPickUp)collisions[i]).Grab();
                _lives = 2;
                /*AddChild(shield);*/
                shieldOn = true;
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
            if (collisions[i] is Wall && isBoosting)
            {
                ((Wall)collisions[i]).Destroy();
            }
            if (collisions[i] is LoadingZone)
            {
                ((LoadingZone)collisions[i]).thisManager.LoadLevelNow();
                ((LoadingZone)collisions[i]).Destroy();
            }
            if (collisions[i] is BoostPad)
            {
                Boost();
                fuel += 20;
                Wall.WallTrigger = true;
            }
        }
    }

    void HandleWalls()
    {
        Collision colx = MoveUntilCollision(velocity.x, 0);
        Collision coly = MoveUntilCollision(0, velocity.y);
        if (coly != null)
        {
            if (coly.normal.y > 0)
            {
                velocity.y = 0;
                _position.y += _speed + 1;
                _mygame.dead = true;
            }
            if (coly.normal.y < 0)
            {
                velocity.y = 0;
                velocity.x = 0;
                rotation = 0;
                _mygame.dead = true;
            }
        }
        if (colx != null)
        {
            if (colx.normal.x > 0)
            {
                velocity.x = 0;
                _position.x += 1;
                rotation = 0;
                _mygame.dead = true;
            }
            if (colx.normal.x < 0)
            {
                velocity.x = 0;
                _position.x -= 1;
                rotation = 0;
                _mygame.dead = true;
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