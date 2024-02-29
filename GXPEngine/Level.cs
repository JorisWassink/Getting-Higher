using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using static TiledMapParser.TiledLoader;

namespace GXPEngine { 
            

    internal class Level : GameObject
    {
        Player player;
        Spikes spike;
        EnemyTurn turn;
        Wall wall;
        Shooter shooter;
        TiledLoader loader;
        RotatingSpaceship _mygame;
        LevelManager levelManager;
        EasyDraw menu;
        Font rowdies;
        
        public string file;

        public Level(LevelManager manag, string thislevelName, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
        {
            _mygame = (RotatingSpaceship)game;
            levelManager = manag;
            loader = new TiledLoader(thislevelName, null, addColliders, defaultOriginX, defaultOriginY);
            loader.autoInstance = true;
            loader.rootObject = this;
            loader.addColliders = false;
            Background background = new Background(1366, 640);
            AddChild(background);
            loader.LoadImageLayers();
            loader.LoadTileLayers(0);
            loader.addColliders = true;
            loader.LoadTileLayers(1);
            loader.LoadObjectGroups(); // player is made -> child of Level
            y -= defaultOriginY;
            player = FindObjectOfType<Player>();
            spike = FindObjectOfType<Spikes>();
            turn = FindObjectOfType<EnemyTurn>();
            wall = FindObjectOfType<Wall>();
            shooter = FindObjectOfType<Shooter>();
            file = thislevelName;

            HighScore();
        }

        void HighScore()
        {
            StreamReader highScoreReader = new StreamReader("Assets/highscore.txt");
            string highScoreText = highScoreReader.ReadLine();
            if (levelManager.loadNumber == -1)
            {
                rowdies = Utils.LoadFont("Assets/Rowdies-Regular.ttf", 40);
                menu = new EasyDraw(1000, 200, false);
                menu.TextFont(rowdies);
                menu.Fill(Color.Yellow);
                menu.TextAlign(CenterMode.Center, CenterMode.Center);
                menu.Text("GETTING HIGHER" + " \nCURRENT HIGHSCORE:" + highScoreText);
                menu.SetOrigin(menu.TextWidth("HIGHSCORE:" + highScoreText)/2, menu.TextHeight("HIGHSCORE:" + "\n GETTING") / 2);
                menu.SetXY(_mygame.width/3, _mygame.height/3);
                LateAddChild(menu);
            }
            highScoreReader.Close();
        }

        void Update()
        {
            Console.WriteLine(Input.mouseX + " " + Input.mouseY);
        }
    }
}