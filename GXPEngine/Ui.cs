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
    Font rowdies;
    public Ui()
    {
        rowdies = Utils.LoadFont("Assets/Rowdies-Regular.ttf", 40);
        fuelBar = new EasyDraw(250, 60, false);
        fuelBar.TextFont(rowdies);
        fuelBar.TextAlign(CenterMode.Min, CenterMode.Center);
        fuelBar.Fill(0, 255, 0);
        fuelBar.Text("Fuel: 100");
        fuelBar.SetXY(10, 10);
        /*Console.WriteLine(fuelBar);*/
        AddChild(fuelBar);


    }
    public void SetFuel(float fuelCount)
    {
        fuelBar.Text(String.Format("Fuel: " + fuelCount), true);
    }
    void Update()
    {
    }
}
