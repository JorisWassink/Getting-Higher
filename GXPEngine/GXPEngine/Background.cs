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


class Background : EasyDraw
{

    EasyDraw currentWave;
    RotatingSpaceship _mygame;
    Sprite ship;
    Random random;
    int starX;
    int starY;

    public int bullets = 10;
    public int kills = 0;
    public int wave = 1;
    public int level;

    public Background(int owidth, int oheight) : base(owidth, oheight, false)
    {
        width = owidth; height = oheight;
        InitializeHUD();
        for (int i = 0; i < 400; i += 1)
        {
            // Calculate the offset based on the current iteration

            // Display current wave text
            starX = random.Next(0, width);
            starY = random.Next(0, height);

            currentWave.Rect(starX, starY, 10, 10);
            ship = new Sprite("Assets/spaceship.png", false, false);
            ship.x = starX; ship.y = starY;
            //   currentWave.SetXY(60, 60 + 2); // Adjusted the Y coordinate with the offset
            AddChild(currentWave);

            // Console.WriteLine("uh oh");


        }

    }


    public void InitializeHUD()
    {
        _mygame = (RotatingSpaceship)game;
        if (!_mygame.dead)
        {
            currentWave = new EasyDraw(width, height, false);
            currentWave.ShapeAlign(CenterMode.Min, CenterMode.Min);
            currentWave.Fill(Color.Yellow);
            currentWave.NoStroke();
            random = new Random((int)(DateTime.Now.Ticks));
        }
        }

        public void Update()
    {


    }
}
