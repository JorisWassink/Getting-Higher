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
            starX = random.Next(0, width);
            starY = random.Next(0, height);

            currentWave.Rect(starX, starY, 10, 10);
            currentWave.blendMode = BlendMode.LIGHTING;
            //   currentWave.SetXY(60, 60 + 2); // Adjusted the Y coordinate with the offset
            AddChild(currentWave);
        }

    }


    public void InitializeHUD()
    {
        _mygame = (RotatingSpaceship)game;
        if (!_mygame.dead)
        {
            currentWave = new EasyDraw(1376, 640, false);
            currentWave.ShapeAlign(CenterMode.Min, CenterMode.Min);
            currentWave.NoStroke();
            currentWave.Fill(Color.Yellow);
            random = new Random((int)(DateTime.Now.Ticks));
        }
    }
    public void DeathEffect()
    {
        currentWave.Fill(Color.Red);
    }
    public void Update()
    {
        currentWave.Fill(Color.Red);
    }

   
}

