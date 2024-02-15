using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace GXPEngine { 
            

    internal class Level : GameObject
    {
        string levelName;
        Player player;
        Sound music;
        TiledLoader loader;

        public Level(string thislevelName)
        {
           
            loader = new TiledLoader(thislevelName);
            Background background = new Background(loader.map.Width * loader.map.TileWidth, loader.map.Height * loader.map.TileHeight);
            AddChild(background);
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
            int boundarySize = 600;

            if (player != null)
            {
                if (player.y + y < boundarySize)
                {
                    y = boundarySize - player.y;
                }
                if (player.y + this.y > game.height - boundarySize)
                {
                    this.y = game.height - boundarySize - player.y;
                }

                // Cap scrolling within the boundary limits


                if (y > -300)
                {
                    y = -300;
                }


                if (y < -(loader.map.Height * loader.map.TileHeight - boundarySize * 1.33f))
                {
                    y = -(loader.map.Height * loader.map.TileHeight) + boundarySize * 1.33f;
                }

                y += 300;

                //Console.WriteLine(y);
            }
            else
            {
                Console.WriteLine("Player not found, scrolling not possible");
            }

        }
    }
}