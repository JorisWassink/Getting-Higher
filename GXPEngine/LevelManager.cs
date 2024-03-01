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
    private string[] levels = new string[29];
   // List<Level> Levels = new List<Level>();



    public LevelManager()
    {
        _mygame = (RotatingSpaceship)game;

        //MENU
        levels[0] = "Assets/empty.tmx";

        //FIRST LEVEL
        levels[1] = "Assets/lvl0.tmx";

        //EASY LEVELS
        levels[2] = "Assets/lvl1.tmx";
        levels[3] = "Assets/lvl2.tmx";
        levels[4] = "Assets/lvl3.tmx";
        levels[5] = "Assets/lvl4.tmx";
        levels[6] = "Assets/lvl5.tmx";
        levels[7] = "Assets/lvl6.tmx";
        levels[8] = "Assets/lvl7.tmx";

        //TRANSITION
        levels[9] = "Assets/lvl8.tmx";

        //NORMAL LEVELS
        levels[10] = "Assets/lvl9.tmx";
        levels[11] = "Assets/lvl10.tmx";
        levels[12] = "Assets/lvl11.tmx";
        levels[13] = "Assets/lvl12.tmx";
        levels[14] = "Assets/lvl13.tmx";
        levels[15] = "Assets/lvl14.tmx";
        levels[16] = "Assets/lvl15.tmx";
        levels[17] = "Assets/lvl16.tmx";

        //HARD LEVELS
        levels[18] = "Assets/lvl17.tmx";
        levels[19] = "Assets/lvl18.tmx";
        levels[20] = "Assets/lvl19.tmx";
        levels[21] = "Assets/lvl20.tmx";
        levels[22] = "Assets/lvl21.tmx";
        levels[23] = "Assets/lvl22.tmx";
        levels[24] = "Assets/lvl23.tmx";
        levels[25] = "Assets/lvl24.tmx";
        levels[26] = "Assets/lvl25.tmx";
        levels[27] = "Assets/lvl26.tmx";
        levels[28] = "Assets/lvl27.tmx";
        StartGame();
    }

    public void StartGame()
    {
        Console.WriteLine("starting game...");
        loadNumber--;
        LoadLevel(levels[0], false, .5f, .5f);
    }

    public void DestroyAll()
    {

        Level[] levels = FindObjectsOfType<Level>();
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].LateDestroy();
        }
        /*player = FindObjectOfType<Player>();*/

        /*Console.WriteLine("Game cleared");*/
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
            //onMenu = false;
        }



        for (int i = 0; i < levelObjects.Count; i++)
        {
            Level level = levelObjects[i];
            if (player != null)
            {
                float dist = player.y - level.y;
                if (dist < -900 && level.file != "Assets/LevelChunk1.tmx")
                {
                    // Console.WriteLine("Deleting: " + level.file);
                    level.Destroy();

                }
            }
            else if (gameStart)
            {
                DestroyAll();
                level.Destroy();
                gameStart = false;
                LoadLevel(levels[1], true, .5f, .5f);
                //LoadLevel(levels[2], true, 0.5f, 1280);
                random = new Random((int)(DateTime.Now.Ticks));
                onMenu = false;


            }
        }

        if (player != null)
        {
            int count = GetChildCount();
            SetChildIndex(player, count);
        }

    }

    public void LoadLevelNow()
    {
        //Console.WriteLine(loadNumber);
        switch (loadNumber)
        {
            case 1:
                LoadLevel(levels[2], true, 0.5f, 1280 * loadNumber);
                break;

            case 2:
                LoadLevel(levels[3], true, 0.5f, 1280 * loadNumber);
                break;

            case 3:
                LoadLevel(levels[4], true, 0.5f, 1280 * loadNumber);
                break;

            case 4:
                LoadLevel(levels[5], true, 0.5f, 1280 * loadNumber);
                break;

            case int n when n > 4 && n < 12:
                LoadLevel(levels[random.Next(5, 9)], true, 0.5f, 1280 * loadNumber);
                break;

            case 12:
                LoadLevel(levels[9], true, 0.5f, 1280 * loadNumber);
                break;

            case int n when n > 12 && n < 20:
                LoadLevel(levels[random.Next(10, 18)], true, 0.5f, 1280 * loadNumber);
                break;
            case 20:
                LoadLevel(levels[19], true, 0.5f, 1280 * loadNumber);
                break;
            case int n when n > 20:
                LoadLevel(levels[random.Next(20, 28)], true, 0.5f, 1280 * loadNumber);
                break;
            default:
                //LoadLevel(levels[0]);
                break;
        }

    }

    public void LoadLevel(string name, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
    {
        Level level = new Level(this, name, addColliders, defaultOriginX, defaultOriginY);
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
