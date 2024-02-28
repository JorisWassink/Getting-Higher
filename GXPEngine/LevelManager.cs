using System;
using System.Collections.Generic;
using System.Linq;
using GXPEngine;
using TiledMapParser;
using static TiledMapParser.TiledLoader;

internal class LevelManager : GameObject
{
    private RotatingSpaceship _mygame;
    private Player player;
    private Random random;
    Ui ui;
    public int loadNumber = 0;
    public bool onMenu = true;
    private bool gameStart = false;
    //Level[] levelObjects;
    List<Level> levelObjects = new List<Level>();
    private string[] levels = new string[7];
   // List<Level> Levels = new List<Level>();

    public LevelManager()
    {
        _mygame = (RotatingSpaceship)game;

        levels[0] = "Assets/empty.tmx";
        levels[1] = "Assets/LevelChunk1.tmx";
        levels[2] = "Assets/LevelChunk2.tmx";
        levels[3] = "Assets/LevelChunk3.tmx";
        levels[4] = "Assets/LevelChunk4.tmx";
        levels[5] = "Assets/LevelChunkRest.tmx";
        levels[6] = "Assets/LevelChunkSpikes.tmx";
        /*        levels[7] = "Assets/level2_try1_pt0.tmx";
                levels[8] = "Assets/level3_try1_pt0.tmx";
                levels[9] = "Assets/level4_try1_pt0.tmx";*/
        StartGame();

        player = FindObjectOfType<Player>();
    }

    public void StartGame()
    {
        Console.WriteLine("starting game...");
        /*loadNumber--;*/
        /*LoadLevel(levels[0], true, .5f, .5f);*/
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
        else if (Input.GetKeyUp(Key.ENTER) && !gameStart)
        {
            gameStart = true;
            /*onMenu = false;*/
        }

        for (int i = 0; i < levelObjects.Count; i++)
        {
            Level level = levelObjects[i];   
            if (player != null)
            {
                float dist = player.y - level.y;
                if (dist < -1000 && level.file != "Assets/LevelChunk1.tmx")
                {
                    Console.WriteLine("Deleting: " + level.file);
                    level.Destroy();

                }
            }
/*            else if(gameStart)
            {
                    DestroyAll();
                    level.Destroy();
                    gameStart = false;
                    LoadLevel(levels[1], true, .5f, .5f);
                    LoadLevel(levels[2], true, 0.5f, 640);
                    random = new Random((int)(DateTime.Now.Ticks));
                    onMenu = false;
            }*/
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
        LoadLevel(levels[random.Next(2, 6)], true, .5f, 1280 * loadNumber);
        Console.WriteLine("Level loaded");
    }

    public void LoadLevel(string name, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
    {
        Level level = new Level(name, addColliders, defaultOriginX, defaultOriginY);
        LateAddChild(level);
        LoadingZone zone = new LoadingZone(0, level.y, 1366, 500, this);
        LateAddChild(zone);
        levelObjects.Add(level);
        loadNumber++;
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

        y += 420;
    }
}
