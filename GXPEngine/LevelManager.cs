using System;
using System.Collections.Generic;
using GXPEngine;
using TiledMapParser;
using static TiledMapParser.TiledLoader;

internal class LevelManager : GameObject
{
    private RotatingSpaceship _mygame;
    private Player player;
    private Random random;
    private string[] levels = new string[6];
    private int loadNumber = 0;

    public LevelManager()
    {
        _mygame = (RotatingSpaceship)game;

        levels[0] = "Assets/empty.tmx";
        levels[1] = "Assets/LevelChunk1.tmx";
        levels[2] = "Assets/LevelChunk2.tmx";
        levels[3] = "Assets/LevelChunk3.tmx";
        levels[4] = "Assets/LevelChunk4.tmx";
        levels[5] = "Assets/LevelChunk5.tmx";

        StartGame();
    }

    public void StartGame()
    {
        Console.WriteLine("starting game...");
        LoadLevel(levels[1], true, .5f, .5f);
        LoadLevel(levels[2], true, 0.5f, 640);

        random = new Random((int)(DateTime.Now.Ticks));
    }

    public void DestroyAll()
    {
        Level[] levels = FindObjectsOfType<Level>();
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].LateDestroy();
        }

        Console.WriteLine("Game cleared");
    }

    public void Update()
    {
        player = FindObjectOfType<Player>();

        if (player != null && player.pInput)
        {
            Scroll();
        }

        Level[] levelObjects = FindObjectsOfType<Level>();
        foreach (var level in levelObjects)
        {
            float dist = player.y - level.y;
            if (dist < -2000 && level.file != "Assets/LevelChunk1.tmx")
            {
                Console.WriteLine("Deleting: " + level.file);
                level.Destroy();
               
            }
        }

        player = FindObjectOfType<Player>();
        int count = GetChildCount();

        if (player != null)
        {
            SetChildIndex(player, count); // int.MaxValue means the highest rendering order
        }
    }

    public void LoadLevelNow()
    {
        LoadLevel(levels[random.Next(2, 5)], true, .5f, 640 * loadNumber);
        Console.WriteLine("Level loaded");
    }

    public void LoadLevel(string name, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
    {
        Level level = new Level(name, addColliders, defaultOriginX, defaultOriginY);
        LateAddChild(level);
        LoadingZone zone = new LoadingZone(0, level.y, 1366, 500, this);
        LateAddChild(zone);
        loadNumber++;
    }
    public void DeathEffect()
    {
        
        
    }

    private void Scroll()
    {
        int boundarySize = 600;

        if (player.y + y < boundarySize)
        {
            this.y = boundarySize - player.y;
        }

        if (player.y + this.y > game.height - boundarySize)
        {
            this.y = game.height - boundarySize - player.y;
        }

        if (y < -(_mygame.height - boundarySize * 0.8f))
        {
            y = -(_mygame.height) + boundarySize * 0.8f;
        }

        y += 410;
    }
}
