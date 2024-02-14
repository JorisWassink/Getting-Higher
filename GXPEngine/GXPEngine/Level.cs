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
            levelName = thislevelName;
            TiledLoader loader = new TiledLoader(thislevelName, null, true);
            loader.rootObject = this;
            loader.LoadImageLayers();
            loader.LoadTileLayers();
            loader.autoInstance = true;
            loader.LoadObjectGroups(); // player is made -> child of Level
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