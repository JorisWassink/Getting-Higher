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
    string[] levels = new string[2];
    public int currentLevel = 0;
    int deathCounter = 180;
    public RotatingSpaceship() : base(1366, 768, false, false)
    {
/*        levels[0] = "Assets/empty.tmx";
        levels[1] = "Assets/LevelChunk1.tmx";
        LoadLevel(levels[1]);
        LoadLevel(levels[0]);*/

        LevelManager manager = new LevelManager();
        AddChild(manager);
        targetFps = 60;

        _spaceship = FindObjectOfType<Player>();

        Ui ui = new Ui();
        LateAddChild(ui);
    }

    void Update()
    {
        if (dead)
        {
            Dead();
        }
    }

    void Dead()
    {
        Console.WriteLine(deathCounter.ToString());
        Background background = FindObjectOfType<Background>();
        background.DeathEffect();
        Player player = FindObjectOfType<Player>();
        player.pDead();
        deathCounter--;
        if (deathCounter == 0)
        {
            deathCounter = 180;
            dead = false;
            LoadLevel(levels[0]);
        }
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
        Ui ui = new Ui();
        LateAddChild(ui);
    }

}