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

namespace GXPEngine { 
            

    internal class Level : GameObject
    {
        Player player;
        Spikes spike;
        EnemyTurn turn;
        Wall wall;
        TiledLoader loader;
        RotatingSpaceship _mygame;
        public string file;

        public Level(string thislevelName, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
        {
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
            file = thislevelName;

            
        }
    }
}