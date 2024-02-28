using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Drawing;
using GXPEngine;

public class RotatingSpaceship : Game
{
    Player _spaceship;
    Ui ui;
    LevelManager manager;
    public bool dead;
    string[] levels = new string[2];
    public int currentLevel = 0;
    int deathCounter = 180;
    public RotatingSpaceship() : base(1366, 768, true, false)
    {


        manager = new LevelManager();
        AddChild(manager);
        targetFps = 60;

        _spaceship = FindObjectOfType<Player>();

        ui = new Ui();
        LateAddChild(ui);


    }

    void Update()
    {
        if (dead)
        {
            Dead();
        }
        string yay = GetDiagnostics();
        Console.WriteLine(yay);
        Console.WriteLine("current fps:" + currentFps);
    }

    public void Dead()
    {
        Background background = FindObjectOfType<Background>();
/*        if (background != null)
        {
            background.DeathEffect();
        }*/
        Player player = FindObjectOfType<Player>();
        player.pDead();
        deathCounter--;
        //fbmanager.DeathEffect();
        /*AnimationSprite sprite = new AnimationSprite("Assets/Space Background.png", 1, 1, -1, false, false);
        sprite.width = width;
        sprite.height = height * 2;
        sprite.blendMode = BlendMode.MULTIPLY;
        int count = GetChildCount();
        SetChildIndex(sprite, count);
        AddChild(sprite);*/
        if (deathCounter == 0)
        {
            deathCounter = 180;
            dead = false;
            manager.Destroy();
            manager = new LevelManager();
            LateAddChild(manager);
            ui.Destroy();
            ui = new Ui();
            LateAddChild(ui);
            
        }
    }

    static void Main()
    {
        new RotatingSpaceship().Start();
    }

}