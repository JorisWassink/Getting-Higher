using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;


class Background : EasyDraw
{
    int count = 0;
    EasyDraw killsDisplayed;
    EasyDraw currentWave;
    EasyDraw currentLevel;

    Font bangers;
    public int bullets = 10;
    public int kills = 0;
    public int wave = 1;
    public int level;

    public Background(int owidth, int oheight, int bullet) : base(owidth, oheight, false)
    {
        bullets = bullet;
        InitializeHUD();

    }


    public void InitializeHUD()
    {
        killsDisplayed = new EasyDraw(width, height, false);
        killsDisplayed.Fill(Color.Yellow);
        killsDisplayed.SetXY(680, 60); // Adjusted the X coordinate to provide equal spacing

    }

    void Update()
    {
        for (int i = 0; i < game.height; i += 10)
        {
            // Calculate the offset based on the current iteration
            int yOffset = i % game.height;
            killsDisplayed.Rect(500, 500,100,100);
            AddChild(killsDisplayed);
            Console.WriteLine(killsDisplayed.y);
        }

    }


}
