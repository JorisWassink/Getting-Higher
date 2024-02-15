using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Drawing;
using GXPEngine;

public class RotatingSpaceship : Game
{
    Player _spaceship;
    EasyDraw _text;
    Ui ui;
    string[] levels = new string[1];
    public int currentLevel = 0;
    public RotatingSpaceship() : base(1920, 1080, false, false)
    {
        levels[0] = "Assets/empty.tmx";
        LoadLevel(levels[0]);

        _spaceship = FindObjectOfType<Player>();

        _text = new EasyDraw(200, 20);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        if (_spaceship != null)
        {
            _text.Text(_spaceship.rotation.ToString(), 0, 0);
        }
        AddChild(_text);


    }

    void Update()
    {
        _text.Clear(Color.Transparent);
        /*_text.Text("Rotation:" + _spaceship.rotation, 0, 0);*/

    }


    static void Main()
    {
        new RotatingSpaceship().Start();
    }


    void LoadLevel(string name)
    {
        List<GameObject> children = GetChildren();

        foreach (GameObject child in children)
        {
            child.Destroy();
        }

        Level level = new Level(name);
        LateAddChild(level);
        ui = new Ui();
        LateAddChild(ui);
    }

}