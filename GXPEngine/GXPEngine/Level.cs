using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace GXPEngine
{
    internal class Level : GameObject
    {
        string levelName;
        Player player;
        Wall wall;
        Sound music;
        public Level(string thislevelName)
        {
            TiledLoader loader = new TiledLoader(thislevelName);
            loader.autoInstance = true;
            loader.rootObject = this;
            loader.addColliders = false;
            loader.LoadImageLayers();
            loader.LoadTileLayers(0);
            loader.addColliders = true;
            loader.LoadTileLayers(1);
            loader.LoadObjectGroups(); // player is made -> child of Level
            player = FindObjectOfType<Player>();
            wall = FindObjectOfType<Wall>();

        }
        void Update()
        {
            scroll();

        }

        void scroll()
        {
            int boundrySize = 700;
            if (player != null)
            {
                if (player.y + y < boundrySize)
                {
                    y    = boundrySize - player.y;
                }
                if (player.y + this.y > game.height - boundrySize)
                {
                    this.y = game.height - boundrySize - player.y;
                }
            }
            else
            {
                Console.WriteLine("Player not found, scrolling not possible");
            }
        }
    }
}