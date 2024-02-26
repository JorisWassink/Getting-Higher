using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using static TiledMapParser.TiledLoader;

  internal class LevelManager : GameObject
    {
        Player player;
        Spikes spike;
        EnemyTurn turn;
        TiledLoader loader;
        RotatingSpaceship _mygame;
     Random random;
        string[] levels = new string[6];
        int loadNumber = 0;
   

    public LevelManager()
        {
        //loader = new TiledLoader(thislevelName, null, addColliders, defaultOriginX, defaultOriginY);
        _mygame = (RotatingSpaceship)game;
            Background background = new Background(1366, 768 * 20);
            LateAddChild(background);



            levels[0] = "Assets/empty.tmx";
            levels[1] = "Assets/LevelChunk1.tmx";
            levels[2] = "Assets/LevelChunk2.tmx";
            levels[3] = "Assets/LevelChunk3.tmx";
            levels[4] = "Assets/LevelChunk4.tmx";
            levels[5] = "Assets/LevelChunk5.tmx";
            LoadLevel(levels[1], true, .5f, .5f);
            LoadLevel(levels[2], true, 0.5f, 640);

            random = new Random((int)(DateTime.Now.Ticks));

    }
        void Update()
        {
        player = FindObjectOfType<Player>();
            if (player != null)
            {
                if (player.pInput)
                {
                    scroll();
                }
            }

            Level[] levels = FindObjectsOfType<Level>();
            for (int i = 0; i < levels.Length; i++)
        {
            float dist = player.y - levels[i].y;
            if (dist < -1000 && levels[i].file != "Assets/LevelChunk1.tmx")
            {
                Console.WriteLine("deleting:" + levels[i].file);
                levels[i].Destroy();
            }
            //Console.WriteLine(dist);
        }

        
    }

        public void LoadLevelNow()
    {
                
   
                LoadLevel(levels[random.Next(2, 5)], true, .5f, 640 * loadNumber);
                Console.WriteLine("level loaded");
    }

        public void LoadLevel(string name, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
        {
            Level level = new Level(name, addColliders, defaultOriginX, defaultOriginY);
            LateAddChild(level);
            LoadingZone zone = new LoadingZone(0, level.y, 1366, 20, this);
            LateAddChild(zone);
            loadNumber++;
        }


    void scroll()
    {
        int boundarySize = 600;

        if (player != null)
        {
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
        else
        {
            Console.WriteLine("Player not found, scrolling not possible");
        }

    }

}
