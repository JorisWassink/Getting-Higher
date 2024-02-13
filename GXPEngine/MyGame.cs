using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using GXPEngine;

public class RotatingSpaceship : Game
{
    Player _spaceship;
    EasyDraw _text;

    public RotatingSpaceship() : base(800, 600, false, false)
    {
        _spaceship = new Player(new Vec2(width / 2, height / 2), "Assets/spaceship.png", 1, 1);
        _spaceship.SetXY(width / 2, height / 2);
        AddChild(_spaceship);

        _spaceship.rotation = 270;

        _text = new EasyDraw(200, 20);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        _text.Text("Rotation: 0", 0, 0);
        AddChild(_text);
    }

    void Update()
    {
        _text.Clear(Color.Transparent);
        _text.Text("Rotation:" + _spaceship.rotation, 0, 0);
    }

    static void Main()
    {
        new RotatingSpaceship().Start();
    }
}