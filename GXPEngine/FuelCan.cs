using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;


    class FuelCan : PickUpBase
    {
        public FuelCan(string imageFile, int cols, int rows, TiledObject obj = null) : base("Assets/jerrycan.png", 1, 1)
        {

        }

        protected override void Initialize(TiledObject obj)
        {
            collider.isTrigger = true;
            SetOrigin(width / 2, height / 2);
        }
    }
