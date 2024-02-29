using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ui : GameObject
{
    EasyDraw fuelBar;
    EasyDraw score;
    EasyDraw deathScreen;
    Font rowdies;
    RotatingSpaceship _mygame;
    public Ui()
    {
        _mygame = (RotatingSpaceship)game;
        rowdies = Utils.LoadFont("Assets/Rowdies-Regular.ttf", 40);
        fuelBar = new EasyDraw(300, 60, false);
        fuelBar.TextFont(rowdies);
        fuelBar.TextAlign(CenterMode.Min, CenterMode.Center);
        fuelBar.Fill(0, 255, 0);
        fuelBar.Text("Fuel: 500");
        fuelBar.SetXY(10, 10);
        AddChild(fuelBar);

        score = new EasyDraw(_mygame.width, 60, false);
        score.TextFont(rowdies);
        score.TextAlign(CenterMode.Min, CenterMode.Center);
        score.Fill(0, 255, 0);
        score.Text("Fuel: 500");
        score.SetXY(_mygame.width - 600, 10);
        AddChild(score);


            deathScreen = new EasyDraw(_mygame.width, _mygame.height, false);
            deathScreen.TextFont(rowdies);
            deathScreen.TextAlign(CenterMode.Center, CenterMode.Center);
            deathScreen.Fill(255, 0, 0);
            deathScreen.Text("u ded");
            deathScreen.SetXY(_mygame.width / 8, _mygame.height / 8);
        
    }



    public void SetFuel(int fuelCount)
    {
        fuelBar.Text(String.Format("Fuel: " + (float)((int)fuelCount)), true);
    }

    public void SetScore(int scoreCount) {
        score.Text(string.Format("score:" + (float)((int)scoreCount)), true);
    }
    void Update()
    {
        if (_mygame.dead && deathScreen != null)
        {
            AddChild(deathScreen);
            RemoveChild(fuelBar);
        }
    }
}
