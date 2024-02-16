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
    public bool dead;
    string[] levels = new string[1];
    public int currentLevel = 0;
    public RotatingSpaceship() : base(1920, 1080, false, false)
    {
        levels[0] = "Assets/empty.tmx";
        LoadLevel(levels[0]);
        targetFps = 60;

        _spaceship = FindObjectOfType<Player>();


    }

    void Update()
    {
        if (dead)
        {
            LoadLevel(levels[0]);
        }
        /*_text.Text("Rotation:" + _spaceship.rotation, 0, 0);*/

    }


    static void Main()
    {
        new RotatingSpaceship().Start();
    }


    void LoadLevel(string name)
    {
        List<GameObject> children = GetChildren();
        dead = false;
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