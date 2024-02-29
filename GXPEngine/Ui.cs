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
    EasyDraw effect;
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
        deathScreen.Stroke(Color.Black);
        deathScreen.StrokeWeight(100);
        deathScreen.TextAlign(CenterMode.Center, CenterMode.Center);
            deathScreen.Fill(255, 0, 0);
            deathScreen.Text("u ded");

           // deathScreen.SetXY(_mygame.width / 2, _mygame.height / 2);
        
        effect = new EasyDraw(_mygame.width, _mygame.height, false);
        effect.ShapeAlign(CenterMode.Min, CenterMode.Min);
        effect.Fill(Color.Black, 100);
        effect.Rect(0,0, _mygame.width, _mygame.height);
        //effect.SetColor(0, 0, 0);
        //effect.blendMode = BlendMode.MULTIPLY;
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
            deathScreen = new EasyDraw(_mygame.width, _mygame.height, false);
            deathScreen.TextFont(rowdies);
            deathScreen.Stroke(Color.Black);
            deathScreen.StrokeWeight(100);
            deathScreen.TextAlign(CenterMode.Center, CenterMode.Center);
            deathScreen.Fill(255, 0, 0);
            deathScreen.Text("u ded");
            //AddChild(effect);
            AddChild(deathScreen);
            RemoveChild(fuelBar);
        }
    }
}
