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
        }
        void Update()
        {
            scroll();            
        }
        void scroll()
        {
            int boundrySize = 500;
            if (player != null)
            {
                if (player.x + x < boundrySize)
                {
                    x = boundrySize - player.x;
                }
                if (player.x + this.x > game.width - boundrySize)
                {
                    this.x = game.width - boundrySize - player.x;
                }
            }
            else
            {
                Console.WriteLine("Player not found, scrolling not possible");
            }
        }
    }
}