using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
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
    Sound boost;
    Sound crash;
    Sound littleFuel;
    Sound noFuel;
    Sound refuel;

    SoundChannel leftChannel;
    SoundChannel rightChannel;
    SoundChannel boostChannel;
    SoundChannel crashChannel;
    SoundChannel littleFuelChannel;
    SoundChannel noFuelChannel;
    SoundChannel refuelChannel;

    RotatingSpaceship _mygame;
    Collider oldCollider;
    AnimationSprite visual;
    EasyDraw shield;

    float shieldX;
    float shieldY;

    float shieldWidth;
    float shieldHeight;

    float _speed;
    float falling;
    float maxVel = 15;
    float gravity = 0.25f;

    float tank = 500;
    public float fuel = 500;

    public float pScore;
    int boostCount;
    public bool isBoosting = false;

    int _lives;
    int timeHit = 0;
    int coolDownTime = 1000;
    StreamWriter highScore;
    StreamReader highScoreReader;
    public string highScoreText;
    bool canCollide = true;

    bool _autoRotateLeft = false;
    bool _autoRotateRight = false;

    bool shieldOn = false;
    public bool pInput = true;
    public Player(string fileName, int cols, int rows, TiledObject obj = null) : base("Assets/Player.png", 1, 1)
    {
        Initialize(obj);
    }

    void Initialize(TiledObject obj)
    {
        _mygame = (RotatingSpaceship)game;
        x = obj.X;
        y = obj.Y;

        oldCollider = _collider;

        _position = new Vec2(x, y);
        SetOrigin(width / 2, height / 2);
        rotation = 270;
        scaleY = 10f;
        scaleX = .3f;
        scale = .5f;
        /*_position.x = game.width / 2;*/
        _speed = 0.7f;
        _lives = 1;
        pScore = position.y;

        rightNoise = new Sound("Assets/Jetpack_middle_left.mp3", true, false);
        leftNoise = new Sound("Assets/Jetpack_middle_right.WAV", true, false);
        boost = new Sound("Assets/BOOST.WAV", false, false);
        crash = new Sound("Assets/Breaking through wood.WAV", false, false);
        littleFuel = new Sound("Assets/Almost out of fuel.WAV", true, false);
        noFuel = new Sound("Assets/Out of fuel.WAV", false, false);
        refuel = new Sound("Assets/Refuel.WAV", false, false);

        rightChannel = (SoundChannel)leftNoise.Play(false, 0, 0);
        leftChannel = (SoundChannel)rightNoise.Play(false, 0, 0);
        littleFuelChannel = (SoundChannel)littleFuel.Play(false, 0, 0);
        noFuelChannel = (SoundChannel)noFuel.Play(false, 0,0);
      

        shieldX = position.x;
        shieldY = position.y;
        shieldWidth = this.width;
        shieldHeight = this.height;
        shield = new EasyDraw(2000, 2000, false);
        shield.Stroke(Color.Black);
        shield.StrokeWeight(5);
        shield.Ellipse(shieldX, shieldY, 350, 500);
        shield.SetXY(-shieldX, -shieldY);
        shield.SetColor(0, 100, 100);
        shield.alpha = 0;
        AddChild(shield);
    }
    void Update()
    {
        /*Console.WriteLine(position);*/
        if (ui == null)
        {
            ui = game.FindObjectOfType<Ui>();
        }

        if (_lives > 1)
        {
            shield.alpha = 0.7f;
        } else
        {
            shield.alpha = 0;
        }

        Movement();
        UpdateScreenPosition();
        HandleBoosting();
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
            /*Console.WriteLine(falling);*/
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
        highScoreReader = new StreamReader("Assets/highscore.txt");
        highScoreText = highScoreReader.ReadLine();
        
        if (-pScore/3 > float.Parse(highScoreText))
        {
            highScoreReader.Close();
            highScore = new StreamWriter("Assets/highscore.txt");
            highScore.WriteLine(Mathf.Round(-pScore/3));
            highScore.Close();
            
        }
        Console.WriteLine(highScoreText);
        if (highScoreReader != null)
        {
            highScoreReader.Close();
        }
        if (pInput && ui != null)
        {
            ui.SetScore(-((int)(pScore) / 3));
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
            leftChannel.Volume = 0.7f;
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
            rightChannel.Volume = 0.7f;
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
                refuel.Play();
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
                if (_lives == 1)
                {
                    shieldOn = false;
                }
            }
            if (collisions[i] is Wall)
            {
                if (isBoosting)
                {
                    ((Wall)collisions[i]).Destroy();
                    crash.Play();
                }
                else
                {
                    _lives -= 1;
                    velocity = velocity * -1;
                    canCollide = false;
                    timeHit = Time.time;
                    if (_lives == 0)
                    {
                        _mygame.dead = true;
                    }
                    if (_lives == 1)
                    {
                        shieldOn = false;
                    }
                }
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
                boost.Play(false, 0, 0.2f);
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
                velocity = velocity * -1;
                _position.y += _speed + 1;
                if (canCollide)
                {
                    _lives -= 1;
                    canCollide = false;
                    timeHit = Time.time;
                    if (_lives == 0)
                    {
                        _mygame.dead = true;
                    }
                    else
                    {
                        shieldOn = false;
                    }
                }
            }
                if (coly.normal.y < 0)
            {
                velocity.y = 0;
                velocity.x = 0;
                rotation = 0;
                }
            }
        if (colx != null)
        {
            if (colx.normal.x > 0)
            {
                velocity.x += 10;
                _position.x += 1;
                rotation = 0;
                if (canCollide)
                {
                    /*_lives -= 1;*/
                    canCollide = false;
                    timeHit = Time.time;
                    if (_lives == 0)
                    {
                        _mygame.dead = true;
                    }
                    if (_lives == 1)
                    {
                        shieldOn = false;
                    }
                }
                }
                if (colx.normal.x < 0)
            {
                velocity.x -= 10;
                _position.x -= 1;
                rotation = 0;
                if (canCollide)
                {
                    /*_lives -= 1;*/
                    canCollide = false;
                    timeHit = Time.time;
                    if (_lives == 0)
                    {
                        _mygame.dead = true;
                    }
                    else
                    {
                        shieldOn = false;
                    }
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
        leftChannel.IsPaused = true;
        rightChannel.IsPaused = true;
        rightChannel.Stop();
        leftChannel.Stop();
        pInput = false;
        gravity = 0;
        if (highScore != null)
        {
            highScore.Close();
        }
    }
}