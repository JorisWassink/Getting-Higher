using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;


class Background : EasyDraw
{
    Player player;
    EasyDraw currentWave;
    EasyDraw background;
    EasyDraw purple;
    RotatingSpaceship _mygame;
    Random random;
    int starX;
    int starY;

    public int bullets = 10;
    public int kills = 0;
    public int wave = 1;
    public int level;

    int r = 0;
    int g = 0;
    int b = 0;

    int startR = 178;
    int startG = 234;
    int startB = 247;

    int targetR = 51;
    int targetG = 83;
    int targetB = 114;

    public Background(int owidth, int oheight) : base(owidth, oheight, false)
    {
        _mygame = (RotatingSpaceship)game;
        width = owidth; height = oheight;
        background = new EasyDraw(1376, 1280, false);
        /*InitializeHUD();
        for (int i = 0; i < 40; i += 1)
        {
            starX = random.Next(0, width);
            starY = random.Next(0, height);

            currentWave.Rect(starX, starY, 10, 10);
            currentWave.blendMode = BlendMode.MULTIPLY;
            //   currentWave.SetXY(60, 60 + 2); // Adjusted the Y coordinate with the offset
            AddChild(currentWave);
        }*/
        //InitializeBackground();

    }


    public void InitializeHUD()
    {
        
        if (!_mygame.dead)
        {
            currentWave = new EasyDraw(1376, 1280, false);
            currentWave.ShapeAlign(CenterMode.Min, CenterMode.Min);
            currentWave.NoStroke();
            currentWave.Fill(Color.Yellow);
            random = new Random((int)(DateTime.Now.Ticks));
        }
    }
    void Update()
    {
        player = _mygame.FindObjectOfType<Player>();
        if (player != null)
        {
            /*     if (player.position.y > 0)
                 {
                     r = (int)(player.position.y / 50);
                     g = (int)(player.position.y / 50);
                     b = (int)(player.position.y / 50);
                 }
                 else
                 {
                     r = -(int)(player.position.y / 50);
                     g = -(int)(player.position.y / 50);
                     b = -(int)(player.position.y / 50);
                 }

                 // Clamp the value of r between 0 and 255
                 r = r < 0 ? 0 : (r > 255 ? 255 : r);
                 g = g < 0 ? 0 : (g > 255 ? 255 : g);
                 b = b < 0 ? 0 : (b > 255 ? 255 : b);*/

            float percentage = -player.position.y / 5000; // Adjust the divisor as needed

            // Interpolate the color values based on the percentage
            int currentR = (int)(startR + (targetR - startR) * percentage);
            int currentG = (int)(startG + (targetG - startG) * percentage);
            int currentB = (int)(startB + (targetB - startB) * percentage);

            // Clamp the interpolated values between 0 and 255
            currentR = (currentR < 0) ? 0 : ((currentR > 255) ? 255 : currentR);
            currentG = (currentG < 0) ? 0 : ((currentG > 255) ? 255 : currentG);
            currentB = (currentB < 0) ? 0 : ((currentB > 255) ? 255 : currentB);

            background.Clear(currentR, currentG, currentB);

            //Console.WriteLine(r);
            background.Clear(currentR, currentG, currentB);
            
        } else
        {
            /*Console.WriteLine("fdsg");*/
        }

    }
    void InitializeBackground()
    {

        background.ShapeAlign(CenterMode.Min, CenterMode.Min);
        background.NoStroke();
        //background.Fill(0, 0, 0);
        background.Rect(x, y, width, height);
       
        //background.blendMode = BlendMode.FILLEMPTY;
        //   currentWave.SetXY(60, 60 + 2); // Adjusted the Y coordinate with the offset
        AddChild(background);

        AnimationSprite sprite = new AnimationSprite("Assets/Space Background.png", 1, 1, -1, false, false);
        sprite.blendMode = BlendMode.LIGHTING;
        AddChild(sprite);

        AnimationSprite sprite2 = new AnimationSprite("Assets/Space Background2.png", 1, 1, -1, false, false);
        sprite2.SetXY(x, sprite.height);
        sprite2.blendMode = BlendMode.LIGHTING;
        AddChild(sprite2);

    }
   
}

